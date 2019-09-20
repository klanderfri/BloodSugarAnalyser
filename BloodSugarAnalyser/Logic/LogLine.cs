using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodSugarAnalyser.Enums;

namespace BloodSugarAnalyser.Logic
{
    /// <summary>
    /// Object representing a log line.
    /// </summary>
    public class LogLine : ILogLine
    {
        /// <summary>
        /// The original raw log line information.
        /// </summary>
        public string RawLine { get; private set; }

        /// <summary>
        /// The index of the line in the file (zero based).
        /// </summary>
        public int LineIndex { get; set; }

        /// <summary>
        /// The index of the log line.
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// The time when the log line was created.
        /// </summary>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// The blood sugar (mmol/L).
        /// </summary>
        public decimal? GlucoseValue { get; set; }

        /// <summary>
        /// The type of log line.
        /// </summary>
        public LogLineType LineType { get; set; }

        /// <summary>
        /// The type of event that caused the creation of the data log line.
        /// </summary>
        public LogEventType DataEventType { get; set; }

        /// <summary>
        /// Tells if the log line holds a blood sugar value.
        /// </summary>
        public bool IsGlucoseLog
        {
            get
            {
                var bloodSugarLogs = new HashSet<LogEventType>() { LogEventType.GlucoseMeasurement, LogEventType.Calibration };
                return bloodSugarLogs.Contains(DataEventType);
            }
        }

        /// <summary>
        /// Creates an object representing a log line.
        /// </summary>
        /// <param name="rawLine">The original raw log line information.</param>
        public LogLine(string rawLine)
        {
            RawLine = rawLine;
        }

        /// <summary>
        /// Checks that the available data corresponds with the type of log line.
        /// </summary>
        public void CheckIntegrity()
        {
            if (IsGlucoseLog && LineType != LogLineType.DataLine)
            {
                throw new ConstraintException("A blood sugar log must be a data log line.");
            }
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
