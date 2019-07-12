using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace BloodSugarAnalyser
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
        private ClarityLogLine LastLogLine { get; set; }

        /// <summary>
        /// The last line with a blood sugar value, the analyser analysed.
        /// </summary>
        private ClarityLogLine LastGlucoseLogLine { get; set; }

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
            var lastIndex = 0;

            try
            {
                var hasIgnoredTitleRow = false;

                //Analyse the file, row by row (to prevent memory problems when analysing big files).
                foreach (var line in File.ReadAllLines(Filepath))
                {
                    //Ignore the first line. It's just a header row.
                    if (!hasIgnoredTitleRow)
                    {
                        hasIgnoredTitleRow = true;
                        continue;
                    }

                    //Analyse the data in the log line.
                    var logLine = new ClarityLogLine(line);
                    analyseLogLine(logLine);

                    //Store the last line index to add to any raised exception.
                    lastIndex = logLine.Index;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("LastLogIndex", lastIndex);
                throw;
            }
        }

        /// <summary>
        /// Analyses a log line and add the information to the log analyse result.
        /// </summary>
        /// <param name="logLine">The log line to analyse.</param>
        private void analyseLogLine(ClarityLogLine logLine)
        {
            //Verify the log line order to prevent lines in disorder.
            if (logLine.Index <= (LastLogLine?.Index ?? 0))
            {
                throw new ConstraintException("The log line index indicates lines in disorder.");
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
            PatientInfo.StoreInfo(logLine);
            analyseBloodSugerData(logLine);

            //Store the current line to be used as the last line in the next iteration.
            LastLogLine = logLine;
        }

        /// <summary>
        /// Analyse the log line data about the blood suger.
        /// </summary>
        /// <param name="logLine">The log line to analyse.</param>
        private void analyseBloodSugerData(ClarityLogLine logLine)
        {
            //Ignore logs not about blood sugar.
            var bloodSugarLogs = new HashSet<string>() { "EGV", "Calibration" };
            if (!bloodSugarLogs.Contains(logLine.EventType)) { return; }

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
        /// <param name="bloodSugar1">The first blood sugar log.</param>
        /// <param name="bloodSugar2">The second blood sugar log.</param>
        /// <param name="inclusiveTopLimit">The inclusive limit of good blood sugar.</param>
        /// <returns>The area above the top limit (seconds * mmol/L).</returns>
        private static decimal calculateAreaAboveRange(ClarityLogLine log1, ClarityLogLine log2, decimal inclusiveTopLimit)
        {
            //Both values are BELOW the limit.
            if (log1.GlucoseValue <= inclusiveTopLimit && log2.GlucoseValue <= inclusiveTopLimit)
            {
                return 0;
            }

            //Both values are ABOVE the limit.
            else if (log1.GlucoseValue > inclusiveTopLimit && log2.GlucoseValue > inclusiveTopLimit)
            {
                var hoursBetween = calculateTimeDifferenceInHours(log1.Timestamp.Value, log2.Timestamp.Value);
                var minValue = Math.Min(log1.GlucoseValue.Value, log2.GlucoseValue.Value);
                var area = (minValue - inclusiveTopLimit) * hoursBetween;

                var maxValue = Math.Max(log1.GlucoseValue.Value, log2.GlucoseValue.Value);
                var triangleArea = (maxValue - minValue) * hoursBetween / 2;
                area += triangleArea;

                if (area < 0) { throw new ConstraintException("The area calculated was a negative value."); }
                return area;
            }

            //One value is above the limit and the other is below.
            else
            {
                //Find the line traversing the blood sugar values.
                var hoursBetween = calculateTimeDifferenceInHours(log1.Timestamp.Value, log2.Timestamp.Value);
                var p1 = new PointF(0, (float)(log1.GlucoseValue));
                var p2 = new PointF((float)hoursBetween, (float)(log2.GlucoseValue));
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
    }
}
