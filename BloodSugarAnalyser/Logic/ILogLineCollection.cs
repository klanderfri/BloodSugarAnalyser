using BloodSugarAnalyser.Data;
using System.Collections.Generic;

namespace BloodSugarAnalyser.Logic
{
    /// <summary>
    /// A collection holding the log lines in a blood sugar log file.
    /// </summary>
    interface ILogLineCollection
    {
        /// <summary>
        /// Gets the type of the log file.
        /// </summary>
        ExportDataType Type { get; }
        
        /// <summary>
        /// Yields the lines in the log files.
        /// </summary>
        /// <returns>Iterator to the lines in the log file.</returns>
        IEnumerable<LogLine> ReadLines();
    }
}
