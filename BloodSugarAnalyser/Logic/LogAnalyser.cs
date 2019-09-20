using BloodSugarAnalyser.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using BloodSugarAnalyser.Enums;
using System.Windows.Forms;

namespace BloodSugarAnalyser.Logic
{
    /// <summary>
    /// Class for object analysing a blood sugar log.
    /// </summary>
    class LogAnalyser
    {
        /// <summary>
        /// Information about the patient the log belongs to.
        /// </summary>
        public PatientInfo PatientInfo { get; private set; }

        /// <summary>
        /// The last log line the analyser analysed.
        /// </summary>
        private ILogLine LastLogLine { get; set; }

        /// <summary>
        /// The last value from a glucose line the analyser analysed.
        /// </summary>
        private decimal? LastGlucoseValue { get; set; }
        /// <summary>
        /// The last timestamp from a glucose line the analyser analysed.
        /// </summary>
        private DateTime? LastGlucoseTimestamp { get; set; }

        /// <summary>
        /// The area of blood sugar above the top limit (EBS, seconds * mmol/L).
        /// </summary>
        public decimal AreaAboveRange { get; private set; }

        /// <summary>
        /// The average area of blood sugar above the top limit per day (average EBS, seconds * mmol/L/Day).
        /// </summary>
        public decimal AverageAreaAboveRangePerDay { get; private set; }

        /// <summary>
        /// The time of the first log line with a time stamp.
        /// </summary>
        public DateTime? TimeStampStart { get; private set; }

        /// <summary>
        /// The time of the last log line with a time stamp.
        /// </summary>
        public DateTime? TimeStampEnd { get; private set; }

        /// <summary>
        /// The inclusive top limit of good blood sugar.
        /// </summary>
        /// <remarks>This value is included within the range of good blood sugar.</remarks>
        public readonly decimal InclusiveTopLimit;

        /// <summary>
        /// The full filepath to the blood sugar log.
        /// </summary>
        public readonly string Filepath;

        /// <summary>
        /// Tells if the analyse should ignore the problem if it finds line indexes in the wrong order.
        /// </summary>
        private bool IgnoreIndexesInDisorder { get; set; }

        /// <summary>
        /// Creates an object analysing a blood sugar log.
        /// </summary>
        /// <param name="filepath">The full filepath to the blood sugar log.</param>
        /// <param name="inclusiveTopLimit">The inclusive top limit of good blood sugar.</param>
        public LogAnalyser(string filepath, decimal inclusiveTopLimit)
        {
            //Initiate member values.
            Filepath = filepath;
            InclusiveTopLimit = inclusiveTopLimit;
            PatientInfo = new PatientInfo();
            AreaAboveRange = 0;

            //Analyse file.
            analyseFile();
        }

        /// <summary>
        /// Analyses a blood sugar log file.
        /// </summary>
        private void analyseFile()
        {
            ulong lastIndex = 0;
            IgnoreIndexesInDisorder = false;

            try
            {
                //Fetch the line collection.
                var rawLines = File.ReadLines(Filepath);
                var fileType = getFileType(rawLines);
                var lineCollection = getLineCollection(rawLines, fileType);
                
                //Get the method analysing a group of lines with the same timestamp.
                Func<IEnumerable<ILogLine>, bool> analyseTimestampGroup = 
                    lines => analyseLogLines(lines, lineCollection.AssertIndexesAreInOrder, lineCollection.HasStrictIndexOrder);

                //Analyse the log lines.
                analyseLogLines(
                    lineCollection.DataLogLines,
                    analyseTimestampGroup,
                    DateTime.MinValue,
                    true);

                //Store the patient info.
                PatientInfo = lineCollection.PatientInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("LastLogIndex", lastIndex);
                throw;
            }
        }

        /// <summary>
        /// Determines what kind of log file we are working with.
        /// </summary>
        /// <param name="rawLines">An iterator to the lines in the file.</param>
        /// <returns>The type of the log file.</returns>
        private CgmSystem getFileType(IEnumerable<string> rawLines)
        {
            var patientID = rawLines.Skip(1).FirstOrDefault();

            if (String.IsNullOrWhiteSpace(patientID))
            {
                return CgmSystem.Unknown;
            }

            if (patientID.Contains("#") && !patientID.Contains(","))
            {
                return CgmSystem.FreestyleLibre;
            }
            else
            {
                return CgmSystem.DexcomClarity;
            }
        }

        /// <summary>
        /// Gets a collection representing the log line information in the file.
        /// </summary>
        /// <param name="rawLines">The raw log lines from the file.</param>
        /// <param name="fileType">The type of the export file.</param>
        /// <returns>A collection representing the log line information.</returns>
        private ILogLineCollection getLineCollection(IEnumerable<string> rawLines, CgmSystem fileType)
        {
            switch (fileType)
            {
                case CgmSystem.DexcomClarity:
                    return new DexcomClarityLog(rawLines);
                case CgmSystem.FreestyleLibre:
                    return new FreestyleLibreLog(rawLines);
                default:
                    var format = "Unknown format '{0}' of variable '{1}'.";
                    var message = String.Format(format, fileType, nameof(fileType));
                    throw new FormatException(message);
            }
        }

        /// <summary>
        /// Analyses log lines and add the information to the log analyse result.
        /// </summary>
        /// <param name="logLines">The log lines to analyse.</param>
        /// <param name="analyseTimestampGroup">Method analysing a group of log lines with identical timestamps.</param>
        /// <param name="lastTimestamp">The timestamp of the last analysed group.</param>
        /// <param name="continueAnalyse">TRUE if the analyse should continue, else FALSE.</param>
        /// <returns>TRUE if the analyse should continue, else FALSE.</returns>
        private bool analyseLogLines(
            Queue<ILogLine> logLines,
            Func<IEnumerable<ILogLine>, bool> analyseTimestampGroup,
            DateTime lastTimestamp,
            bool continueAnalyse)
        {
            //Check if the analyse should continue.
            if (!continueAnalyse //The analyse needs to stop prematurely.
                || !logLines.Any()) //The analyse is finished when there are no more lines.
            {
                return continueAnalyse;
            }

            //Find the lines with the same timestamp.
            var linesWithSameTimestamp = new List<ILogLine>();
            DateTime currentTimestamp;
            do
            {
                var line = logLines.Dequeue();
                currentTimestamp = line.Timestamp.Value;
                linesWithSameTimestamp.Add(line);

            } while (logLines.Any() && logLines.Peek().Timestamp == currentTimestamp);

            //Verify the timestamp order.
            if (lastTimestamp == currentTimestamp)
            {
                var format = "The parameter {0} contains timestamps indicating the lines should have been analysed in the last run.";
                var message = String.Format(format, nameof(logLines));
                throw new ArgumentException(message, nameof(logLines));
            }
            if (lastTimestamp > currentTimestamp)
            {
                throw new ConstraintException("The log line timestamp indicates lines in disorder.");
            }

            //Analyse the lines.
            continueAnalyse = analyseTimestampGroup(linesWithSameTimestamp);

            //Analyse the rest of the lines.
            return analyseLogLines(logLines, analyseTimestampGroup, currentTimestamp, continueAnalyse);
        }

        /// <summary>
        /// Analyses log lines and add the information to the log analyse result.
        /// </summary>
        /// <param name="logLines">The log lines to analyse (should have identical timestamps).</param>
        /// <param name="indexesAreInOrder">A method verifying the order of the indexes of two log lines.</param>
        /// <param name="hasStrictIndexOrder">Tells if the indexes of the log lines has to come in order.</param>
        /// <returns>TRUE if the analyse should continue, else FALSE.</returns>
        private bool analyseLogLines(
            IEnumerable<ILogLine> logLines,
            Func<ulong, ulong, bool> indexesAreInOrder,
            bool hasStrictIndexOrder)
        {
            //Make sure we actually got some lines to test.
            if (!logLines.Any())
            {
                var format = "The parameter {0} does not contain any items.";
                var message = String.Format(format, nameof(logLines));
                throw new ArgumentException(message, nameof(logLines));
            }

            //All the lines should have the same timestamp.
            var hasMoreThanOneTimestamp = logLines.Select(l => l.Timestamp).GroupBy(t => t).Skip(1).Any();
            if (hasMoreThanOneTimestamp)
            {
                var format = "The parameter {0} contains log lines with more than one timestamp.";
                var message = String.Format(format, nameof(logLines));
                throw new ArgumentException(message, nameof(logLines));
            }

            //Verify the log line order to prevent lines in disorder.
            if (!verifyIdOrder(logLines, indexesAreInOrder, hasStrictIndexOrder))
            {
                return false;
            }

            //Store the last log line.
            if (LastLogLine == null)
            {
                LastLogLine = logLines.First();
            }

            //Store the timestamps.
            if (!TimeStampStart.HasValue)
            {
                TimeStampStart = logLines.First().Timestamp;
            }
            TimeStampEnd = logLines.Where(l => l.Timestamp.HasValue).Last().Timestamp;

            //Analyse the data in the log line.
            analyseBloodSugerData(logLines);

            //Store the current line to be used as the last line in the next iteration.
            LastLogLine = logLines.Last();

            //Continue the analyse.
            return true;
        }

        /// <summary>
        /// Verifies that the order of the ID:s of the provided log lines is valid.
        /// </summary>
        /// <param name="logLines">The log lines to analyse.</param>
        /// <param name="idsAreInOrder">A method verifying the order of the indexes of two log lines.</param>
        /// <param name="hasStrictIndexOrder">Tells if the indexes of the log lines has to come in order.</param>
        /// <returns>TRUE if the overall analyse should continue, else FALSE.</returns>
        private bool verifyIdOrder(IEnumerable<ILogLine> logLines, Func<ulong, ulong, bool> idsAreInOrder, bool hasStrictIndexOrder)
        {
            //We dont care about the order in some cases.
            if (!hasStrictIndexOrder) { return true; }
            if (IgnoreIndexesInDisorder) { return true; }

            //Extract the indexes to check.
            var ids = getPropertyIterator<ulong>(logLines, LastLogLine, "ID");

            //Check indexes.
            var orderIsOK = true;
            var lastID = ids.First();
            foreach (var id in ids.Skip(1))
            {
                if (!idsAreInOrder(lastID, id))
                {
                    orderIsOK = false;
                    break;
                }

                lastID = id;
            }

            //Check if we need to ask the user for input on what to do.
            if (!orderIsOK)
            {
                //TODO: Show this message using events instead.
                var message = "There are line ID:s in the wrong order. This might indicate a faulty file. Do you want to ignore this anomaly and continue?";
                var result = MessageBox.Show(message, "ID:s in disorder", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    IgnoreIndexesInDisorder = true;
                }
                else
                {
                    return false;
                }
            }

            //Continue the analyse.
            return true;
        }

        /// <summary>
        /// Extracts an iterator iterating over a specific property of the provided loglines.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="logLines">The log lines containing the property.</param>
        /// <param name="lastLogLine">The last line that was previously analysed.</param>
        /// <param name="propertyName">The name of the property the iterator should iterate.</param>
        /// <returns>The iterator.</returns>
        private static IEnumerable<T> getPropertyIterator<T>(IEnumerable<ILogLine> logLines, ILogLine lastLogLine, string propertyName)
        {
            var property = logLines.First().GetType().GetProperty(propertyName);
            var properties = new List<T>(logLines.Select(l => (T)property.GetValue(l)));

            if (lastLogLine != null)
            {
                properties.Insert(0, (T)property.GetValue(lastLogLine));
            }

            return properties;
        }

        /// <summary>
        /// Analyse the log line data about the blood suger.
        /// </summary>
        /// <param name="logLines">The log lines to analyse.</param>
        private void analyseBloodSugerData(IEnumerable<ILogLine> logLines)
        {
            var glucoseLines = logLines.Where(l => l.IsGlucoseLog);

            //Ignore logs not about blood sugar.
            if (!glucoseLines.Any()) { return; }

            //Store the first ever log and continue to the next one.
            if (!LastGlucoseTimestamp.HasValue)
            {
                storeLastGlucoseInfo(glucoseLines);
                return;
            }

            //Calculate the area above the top limit.
            AreaAboveRange += calculateAreaAboveRange(
                LastGlucoseValue.Value,
                LastGlucoseTimestamp.Value,
                getAverageGlucose(glucoseLines).Value,
                glucoseLines.Last().Timestamp.Value,
                InclusiveTopLimit);

            //Calculate the average area above the top limit.
            var days = Convert.ToDecimal((TimeStampEnd - TimeStampStart).Value.TotalDays);
            if (days != 0)
            {
                AverageAreaAboveRangePerDay = AreaAboveRange / days;
            }

            //Store the log line for when the next line is analysed.
            storeLastGlucoseInfo(glucoseLines);
        }

        private void storeLastGlucoseInfo(IEnumerable<ILogLine> glucoseLines)
        {
            LastGlucoseTimestamp = glucoseLines.Last().Timestamp;
            LastGlucoseValue = getAverageGlucose(glucoseLines);
        }

        private static decimal? getAverageGlucose(IEnumerable<ILogLine> glucoseLines)
        {
            return glucoseLines.Select(l => l.GlucoseValue).Average();
        }

        /// <summary>
        /// Calculates the area above the top limit for two blood sugar points.
        /// </summary>
        /// <param name="glucoseValue1">The first blood sugar glucose value.</param>
        /// <param name="timestamp1">The timestamp of the first blood sugar.</param>
        /// <param name="glucoseValue2">The second blood sugar glucose value.</param>
        /// <param name="timestamp2">The timestamp of the second blood sugar.</param>
        /// <param name="inclusiveTopLimit">The inclusive limit of good blood sugar.</param>
        /// <returns>The area above the top limit (seconds * mmol/L).</returns>
        private static decimal calculateAreaAboveRange(decimal glucoseValue1, DateTime timestamp1, decimal glucoseValue2, DateTime timestamp2, decimal inclusiveTopLimit)
        {
            //Both values are BELOW the limit.
            if (glucoseValue1 <= inclusiveTopLimit && glucoseValue2 <= inclusiveTopLimit)
            {
                return 0;
            }

            //Both values are ABOVE the limit.
            else if (glucoseValue1 > inclusiveTopLimit && glucoseValue2 > inclusiveTopLimit)
            {
                var hoursBetween = calculateTimeDifferenceInHours(timestamp1, timestamp2);
                var minValue = Math.Min(glucoseValue1, glucoseValue2);
                var area = (minValue - inclusiveTopLimit) * hoursBetween;

                var maxValue = Math.Max(glucoseValue1, glucoseValue2);
                var triangleArea = (maxValue - minValue) * hoursBetween / 2;
                area += triangleArea;

                if (area < 0) { throw new ConstraintException("The area calculated was a negative value."); }
                return area;
            }

            //One value is above the limit and the other is below.
            else
            {
                //Find the line traversing the blood sugar values.
                var hoursBetween = calculateTimeDifferenceInHours(timestamp1, timestamp2);
                var p1 = new PointF(0, (float)(glucoseValue1));
                var p2 = new PointF((float)hoursBetween, (float)(glucoseValue2));
                var line = new MathLine(p1, p2);

                //Find where the line crosses the top limit.
                var topLimitLine = new MathLine(0, (float)inclusiveTopLimit);
                var intersection = MathLine.GetIntersectionPoint(line, topLimitLine);

                //Calculate the area above the top limit.
                var highestPoint = p1.Y > p2.Y ? p1 : p2;
                var triangleBase = Math.Abs(intersection.X - highestPoint.X);
                var triangleHeight = highestPoint.Y - (float)inclusiveTopLimit;
                var triangleArea = triangleBase * triangleHeight / 2;

                //Return the area above the top limit.
                if (triangleArea < 0) { throw new ConstraintException("The area calculated was a negative value."); }
                return Convert.ToDecimal(triangleArea);
            }
        }

        /// <summary>
        /// Calculates the amount of hours that differs the two dates/times.
        /// </summary>
        /// <param name="d1">The first date/time.</param>
        /// <param name="d2">The second date/time</param>
        /// <returns>The absolute difference in hours between the two points in time.</returns>
        private static decimal calculateTimeDifferenceInHours(DateTime d1, DateTime d2)
        {
            return Convert.ToDecimal(Math.Abs((d1 - d2).TotalHours));
        }

        /// <summary>
        /// Returns the log analyse result as a human readable string.
        /// </summary>
        /// <returns>The analyse reasult as a string.</returns>
        public string GetResult()
        {
            var result = new StringBuilder();

            result.AppendFormat(
                "Patient: {0} {1} (PID {2})",
                PatientInfo.FirstName,
                PatientInfo.Surname,
                PatientInfo.PatientID);
            result.AppendLine();

            result.AppendFormat("Total EBS: {0} h*mmol/L", Math.Round(AreaAboveRange, 2));
            result.AppendLine();

            result.AppendFormat("EBS per day: {0} h*mmol/L", Math.Round(AverageAreaAboveRangePerDay, 2));
            result.AppendLine();

            result.AppendFormat("Period start: {0}", TimeStampStart);
            result.AppendLine();

            result.AppendFormat("Period end: {0}", TimeStampEnd);
            result.AppendLine();

            return result.ToString();
        }
    }
}
