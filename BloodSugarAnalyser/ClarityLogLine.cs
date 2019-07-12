using System;
using System.Collections.Generic;
using System.Data;

namespace BloodSugarAnalyser
{
    /// <summary>
    /// Class for object holding the data of a log line.
    /// </summary>
    class ClarityLogLine
    {
        /// <summary>
        /// The index of the log line.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// The time of the log.
        /// </summary>
        public DateTime? Timestamp { get; private set; }
        public string EventType { get; private set; }
        public string EventSubtype { get; private set; }
        public string PatientInfo { get; private set; }
        public string DeviceInfo { get; private set; }
        public string SourceDeviceID { get; private set; }
        /// <summary>
        /// The blood sugar (mmol/L).
        /// </summary>
        public decimal? GlucoseValue { get; private set; }
        /// <summary>
        /// The amount of insulin taken (u).
        /// </summary>
        public decimal? InsulinValue { get; private set; }
        /// <summary>
        /// The amount of carbs eaten (grams).
        /// </summary>
        public int? CarbValue { get; private set; }
        public TimeSpan? Duration { get; private set; }
        /// <summary>
        /// How fast the blood sugar is changing (mmol/L/min).
        /// </summary>
        public decimal? GlucoseRateOfChange { get; private set; }
        public long? TransmitterTime { get; private set; }
        public string TransmitterID { get; private set; }

        public bool IsBloodSugarLog
        {
            get
            {
                var bloodSugarLogs = new HashSet<string>() { "EVG", "Calibration" };
                return bloodSugarLogs.Contains(EventType);
            }
        }

        public ClarityLogLine(string logLine)
        {
            logLine += ",";
            var values = logLine.Split(',');

            Index = Convert.ToInt32(values[0]);
            Timestamp = values[1] == "" ? (DateTime?)null : Convert.ToDateTime(values[1]);
            EventType = values[2] == "" ? null : values[2];
            EventSubtype = values[3] == "" ? null : values[3];
            PatientInfo = values[4] == "" ? null : values[4];
            DeviceInfo = values[5] == "" ? null : values[5];
            SourceDeviceID = values[6] == "" ? null : values[6];
            GlucoseValue = convertToDecimal(values[7]);
            InsulinValue = convertToDecimal(values[8]);
            CarbValue = values[9] == "" ? (int?)null : Convert.ToInt32(values[9]);
            Duration = values[10] == "" ? (TimeSpan?)null : TimeSpan.Parse(values[10]);
            GlucoseRateOfChange = convertToDecimal(values[11]);
            TransmitterTime = values[12] == "" ? (long?)null : Convert.ToInt64(values[12]);
            TransmitterID = values[13] == "" ? null : values[13];

            checkIntegrity();
        }

        private void checkIntegrity()
        {
            if (IsBloodSugarLog && GlucoseValue == null)
            {
                throw new ConstraintException("A blood sugar log without any blood sugar value was found.");
            }
            if (IsBloodSugarLog && Timestamp == null)
            {
                throw new ConstraintException("A blood sugar log without any timestamp was found.");
            }
        }

        /// <summary>
        /// Converts a Clarity input string to decimal.
        /// </summary>
        /// <param name="input">The text string to convert.</param>
        /// <returns>A decimal value.</returns>
        private decimal? convertToDecimal(string input)
        {
            if (input == "")
            {
                return null;
            }
            else
            {
                input = input
                    .Replace("Low", "0")
                    .Replace('.', ',');
                return Convert.ToDecimal(input);
            }
        }
    }
}
