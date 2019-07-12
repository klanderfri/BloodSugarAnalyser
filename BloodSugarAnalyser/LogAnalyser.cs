using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;

namespace BloodSugarAnalyser
{
    class LogAnalyser
    {
        public PatientInfo PatientInfo { get; private set; }
        private ClarityLogLine LastLogLine { get; set; }
        public decimal AreaAboveRange { get; private set; }
        public DateTime TimeStampStart { get; private set; }
        public DateTime TimeStampEnd
        {
            get { return LastLogLine.Timestamp.Value; }
        }
        public decimal InclusiveTopLimit { get; private set; }
        public decimal AverageAreaAboveRangePerDay
        {
            get
            {
                var days = Convert.ToDecimal((TimeStampEnd - TimeStampStart).TotalDays);
                return AreaAboveRange / days;
            }
        }

        public LogAnalyser(decimal inclusiveTopLimit)
        {
            PatientInfo = new PatientInfo();
            AreaAboveRange = 0;
            InclusiveTopLimit = inclusiveTopLimit;
        }

        public void AnalyseLogLine(ClarityLogLine logLine)
        {
            PatientInfo.StoreInfo(logLine);
            calculateOutOfRangeArea(logLine, InclusiveTopLimit);
        }

        private void calculateOutOfRangeArea(ClarityLogLine logLine, decimal inclusiveTopLimit)
        {
            //Ignore logs not about blood sugar.
            var bloodSugarLogs = new HashSet<string>() { "EGV", "Calibration" };
            if (!bloodSugarLogs.Contains(logLine.EventType)) { return; }

            //Store the first log and continue to the next one.
            if (LastLogLine == null)
            {
                LastLogLine = logLine;
                TimeStampStart = logLine.Timestamp.Value;
                return;
            }
            
            AreaAboveRange += calculateAreaAboveRange(inclusiveTopLimit, LastLogLine, logLine);

            LastLogLine = logLine;
        }

        /// <summary>
        /// Calculates the area above the top limit for two blood sugar points.
        /// </summary>
        /// <param name="inclusiveTopLimit">The inclusive limit of good blood sugar.</param>
        /// <param name="bloodSugar1">The first blood sugar log.</param>
        /// <param name="bloodSugar2">The second blood sugar log.</param>
        /// <returns>The area above the top limit (seconds * mmol/L).</returns>
        private decimal calculateAreaAboveRange(decimal inclusiveTopLimit, ClarityLogLine log1, ClarityLogLine log2)
        {
            //Both values are BELOW the limit.
            if (log1.GlucoseValue <= inclusiveTopLimit && log2.GlucoseValue <= inclusiveTopLimit) { return 0; }

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

        private decimal calculateTimeDifferenceInHours(DateTime d1, DateTime d2)
        {
            return Convert.ToDecimal(Math.Abs((d1 - d2).TotalHours));
        }
    }
}
