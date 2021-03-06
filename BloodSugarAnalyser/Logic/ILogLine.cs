﻿using BloodSugarAnalyser.Enums;
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
        /// The index of the line in the file (zero based).
        /// </summary>
        int LineIndex { get; set; }

        /// <summary>
        /// The ID of the log line.
        /// </summary>
        ulong ID { get; set; }

        /// <summary>
        /// The time when the log line was created.
        /// </summary>
        DateTime? Timestamp { get; set; }

        /// <summary>
        /// The blood sugar (mmol/L).
        /// </summary>
        decimal? GlucoseValue { get; set; }

        /// <summary>
        /// The type of log line.
        /// </summary>
        LogLineType LineType { get; set; }

        /// <summary>
        /// The type of event that caused the creation of the data log line.
        /// </summary>
        LogEventType DataEventType { get; set; }

        /// <summary>
        /// Tells if the log line holds a blood sugar value.
        /// </summary>
        bool IsGlucoseLog { get; }

        /// <summary>
        /// Checks the data integrety of the log line and throws an exception if something is wrong.
        /// </summary>
        /// <exception cref="ConstraintException">Exception explaining the data integrity violation found.</exception>
        void CheckIntegrity();
    }
}
