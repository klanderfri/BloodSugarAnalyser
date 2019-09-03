using System;
using System.Collections.Generic;
using System.Data;

namespace BloodSugarAnalyser.Data
{
    /// <summary>
    /// Class for object holding the data of a log line.
    /// </summary>
    public class LogLine
    {
        /// <summary>
        /// The original raw log line information.
        /// </summary>
        public string RawLine { get; private set; }

        /// <summary>
        /// The index of the log line.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The time when the log line was created.
        /// </summary>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// The type of event that caused the creation of the log line.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// The subtype of event that caused the creation of the log line (for example, the type of alarm).
        /// </summary>
        public string EventSubtype { get; set; }

        /// <summary>
        /// Information about the patient.
        /// </summary>
        public string PatientInfo { get; set; }

        /// <summary>
        /// Information about the source device.
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// The ID of the source device.
        /// </summary>
        public string SourceDeviceID { get; set; }

        /// <summary>
        /// The blood sugar (mmol/L).
        /// </summary>
        public decimal? GlucoseValue { get; set; }

        /// <summary>
        /// The amount of insulin taken (u).
        /// </summary>
        public decimal? InsulinValue { get; set; }

        /// <summary>
        /// The amount of carbs eaten (grams).
        /// </summary>
        public int? CarbValue { get; set; }

        /// <summary>
        /// The time duration (for an alarm, for example).
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// How fast the blood sugar is changing (mmol/L/min).
        /// </summary>
        public decimal? GlucoseRateOfChange { get; set; }

        /// <summary>
        /// The internal timestamp of the transmitter.
        /// </summary>
        public long? TransmitterTime { get; set; }

        /// <summary>
        /// The ID of the transmitter.
        /// </summary>
        public string TransmitterID { get; set; }

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
        /// <param name="rawLine">The log line as a raw string (comma separated).</param>
        public LogLine(string rawLine)
        {
            RawLine = rawLine;
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
    }
}
