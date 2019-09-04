using BloodSugarAnalyser.Enums;
using BloodSugarAnalyser.Logic;
using System;
using System.Collections.Generic;
using System.Data;

namespace BloodSugarAnalyser.Data
{
    /// <summary>
    /// Class for object holding the data of a log line.
    /// </summary>
    public class DexcomClarityLogLine : LogLine
    {
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
        /// Creates an object holding the data of a log line.
        /// </summary>
        /// <param name="rawLine">The log line as a raw string (comma separated).</param>
        public DexcomClarityLogLine(string rawLine)
            : base(rawLine) { }
    }
}
