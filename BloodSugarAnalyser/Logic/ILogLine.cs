using BloodSugarAnalyser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodSugarAnalyser.Logic
{
    public interface ILogLine
    {
        /// <summary>
        /// Gets the original log line the object is based on.
        /// </summary>
        string RawLine { get; }

        /// <summary>
        /// The index of the log line.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// The time when the log line was created.
        /// </summary>
        DateTime? Timestamp { get; set; }

        /// <summary>
        /// The blood sugar (mmol/L).
        /// </summary>
        decimal? GlucoseValue { get; set; }

        /// <summary>
        /// The type of event that caused the creation of the log line.
        /// </summary>
        LogEventType EventType { get; set; }

        /// <summary>
        /// Tells if the log line holds a blood sugar value.
        /// </summary>
        bool IsGlucoseLog { get; }

        /// <summary>
        /// Checks the data integrety of the log line and throws an exception if something is wrong.
        /// </summary>
        /// <exception cref="ConstraintException">Exception excplaining the data integrity violation found.</exception>
        void CheckIntegrity();
    }
}
