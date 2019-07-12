using System;
using System.Collections.Generic;
using System.Data;

namespace BloodSugarAnalyser.Data
{
    /// <summary>
    /// Class for object holding the data of a log line.
    /// </summary>
    class LogLine
    {
        /// <summary>
        /// The index of the log line.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// The time when the log line was created.
        /// </summary>
        public DateTime? Timestamp { get; private set; }

        /// <summary>
        /// The type of event that caused the creation of the log line.
        /// </summary>
        public string EventType { get; private set; }

        /// <summary>
        /// The subtype of event that caused the creation of the log line (for example, the type of alarm).
        /// </summary>
        public string EventSubtype { get; private set; }

        /// <summary>
        /// Information about the patient.
        /// </summary>
        public string PatientInfo { get; private set; }

        /// <summary>
        /// Information about the source device.
        /// </summary>
        public string DeviceInfo { get; private set; }

        /// <summary>
        /// The ID of the source device.
        /// </summary>
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

        /// <summary>
        /// The time duration (for an alarm, for example).
        /// </summary>
        public TimeSpan? Duration { get; private set; }

        /// <summary>
        /// How fast the blood sugar is changing (mmol/L/min).
        /// </summary>
        public decimal? GlucoseRateOfChange { get; private set; }

        /// <summary>
        /// The internal timestamp of the transmitter.
        /// </summary>
        public long? TransmitterTime { get; private set; }

        /// <summary>
        /// The ID of the transmitter.
        /// </summary>
        public string TransmitterID { get; private set; }

        /// <summary>
        /// Tells if the log line holds a blood sugar value.
        /// </summary>
        public bool IsGlucoseLog
        {
            get
            {
                var bloodSugarLogs = new HashSet<string>() { "EVG", "Calibration" };
                return bloodSugarLogs.Contains(EventType);
            }
        }

        /// <summary>
        /// Creates an object holding the data of a log line.
        /// </summary>
        /// <param name="rawLogLine">The log line as a raw string (comma separated).</param>
        public LogLine(string rawLogLine)
        {
            rawLogLine += ",";
            var values = rawLogLine.Split(',');

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

        /// <summary>
        /// Checks that the available data corresponds with the type of log line.
        /// </summary>
        private void checkIntegrity()
        {
            if (IsGlucoseLog && GlucoseValue == null)
            {
                throw new ConstraintException("A blood sugar log without any blood sugar value was found.");
            }
            if (IsGlucoseLog && Timestamp == null)
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
