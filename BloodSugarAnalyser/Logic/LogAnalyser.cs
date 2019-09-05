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
        /// The last line with a blood sugar value, the analyser analysed.
        /// </summary>
        private ILogLine LastGlucoseLogLine { get; set; }

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

                //Analyse the file, row by row (to prevent memory problems when analysing big files).
                foreach (var logLine in lineCollection.ReadLines())
                {
                    //Analyse the data in the log line.
                    var continueAnalyse = analyseLogLine(logLine, lineCollection.AssertIndexesAreInOrder, lineCollection.HasStrictIndexOrder);

                    //Store the last line index to add to any raised exception.
                    lastIndex = logLine.Index;

                    //Check if the analyse should continue.
                    if (!continueAnalyse) { break; }
                }

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
        /// Analyses a log line and add the information to the log analyse result.
        /// </summary>
        /// <param name="logLine">The log line to analyse.</param>
        /// <param name="indexesAreInOrder">A method verifying the order of the indexes of two log lines.</param>
        /// <param name="hasStrictIndexOrder">Tells if the indexes of the log lines has to come in order.</param>
        /// <returns>TRUE if the analyse should continue, else FALSE.</returns>
        private bool analyseLogLine(ILogLine logLine, Func<ulong, ulong, bool> indexesAreInOrder, bool hasStrictIndexOrder)
        {
            //Verify the log line order to prevent lines in disorder.
            if (hasStrictIndexOrder && !indexesAreInOrder(LastLogLine?.Index ?? 0, logLine.Index) && !IgnoreIndexesInDisorder)
            {
                //TODO: Show this message using events instead.
                var message = "There are line indexes in the wrong order. This might indicate a faulty file. Do you want to ignore this anomaly and continue?";
                var result = MessageBox.Show(message, "Indexes in disorder", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    IgnoreIndexesInDisorder = true;
                }
                else
                {
                    return false;
                }
            }

            //Verify that the timestamps are in order.
            if (LastLogLine?.Timestamp != null && LastLogLine.Timestamp > logLine.Timestamp)
            {
                throw new ConstraintException("The log line timestamp indicates lines in disorder.");
            }
            if (LastLogLine?.Timestamp != null && LastLogLine.Timestamp == logLine.Timestamp)
            {
                throw new ConstraintException("Two log lines has the same timestamp.");
            }

            //Store the last log line.
            if (LastLogLine == null)
            {
                LastLogLine = logLine;
            }

            //Store the timestamps.
            if (TimeStampStart == null)
            {
                TimeStampStart = logLine.Timestamp;
            }
            if (logLine.Timestamp != null)
            {
                TimeStampEnd = logLine.Timestamp;
            }

            //Analyse the data in the log line.
            analyseBloodSugerData(logLine);

            //Store the current line to be used as the last line in the next iteration.
            LastLogLine = logLine;

            //Continue the analyse.
            return true;
        }

        /// <summary>
        /// Analyse the log line data about the blood suger.
        /// </summary>
        /// <param name="logLine">The log line to analyse.</param>
        private void analyseBloodSugerData(ILogLine logLine)
        {
            //Ignore logs not about blood sugar.
            if (!logLine.IsGlucoseLog) { return; }

            //Store the first log and continue to the next one.
            if (LastGlucoseLogLine == null)
            {
                LastGlucoseLogLine = logLine;
                return;
            }

            //Calculate the area above the top limit.
            AreaAboveRange += calculateAreaAboveRange(LastGlucoseLogLine, logLine, InclusiveTopLimit);

            //Calculate the average area above the top limit.
            var days = Convert.ToDecimal((TimeStampEnd - TimeStampStart).Value.TotalDays);
            if (days != 0)
            {
                AverageAreaAboveRangePerDay = AreaAboveRange / days;
            }

            //Store the log line for when the next line is analysed.
            LastGlucoseLogLine = logLine;
        }

        /// <summary>
        /// Calculates the area above the top limit for two blood sugar points.
        /// </summary>
        /// <param name="logLine1">The first blood sugar log line.</param>
        /// <param name="logLine2">The second blood sugar log line.</param>
        /// <param name="inclusiveTopLimit">The inclusive limit of good blood sugar.</param>
        /// <returns>The area above the top limit (seconds * mmol/L).</returns>
        private static decimal calculateAreaAboveRange(ILogLine logLine1, ILogLine logLine2, decimal inclusiveTopLimit)
        {
            //Both values are BELOW the limit.
            if (logLine1.GlucoseValue <= inclusiveTopLimit && logLine2.GlucoseValue <= inclusiveTopLimit)
            {
                return 0;
            }

            //Both values are ABOVE the limit.
            else if (logLine1.GlucoseValue > inclusiveTopLimit && logLine2.GlucoseValue > inclusiveTopLimit)
            {
                var hoursBetween = calculateTimeDifferenceInHours(logLine1.Timestamp.Value, logLine2.Timestamp.Value);
                var minValue = Math.Min(logLine1.GlucoseValue.Value, logLine2.GlucoseValue.Value);
                var area = (minValue - inclusiveTopLimit) * hoursBetween;

                var maxValue = Math.Max(logLine1.GlucoseValue.Value, logLine2.GlucoseValue.Value);
                var triangleArea = (maxValue - minValue) * hoursBetween / 2;
                area += triangleArea;

                if (area < 0) { throw new ConstraintException("The area calculated was a negative value."); }
                return area;
            }

            //One value is above the limit and the other is below.
            else
            {
                //Find the line traversing the blood sugar values.
                var hoursBetween = calculateTimeDifferenceInHours(logLine1.Timestamp.Value, logLine2.Timestamp.Value);
                var p1 = new PointF(0, (float)(logLine1.GlucoseValue));
                var p2 = new PointF((float)hoursBetween, (float)(logLine2.GlucoseValue));
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
