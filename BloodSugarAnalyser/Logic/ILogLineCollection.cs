using BloodSugarAnalyser.Data;
using BloodSugarAnalyser.Enums;
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
        /// Gets the information about the patient who the log belongs to.
        /// </summary>
        /// <remarks>The patient info will be empty if the read lines method havn't been called.</remarks>
        PatientInfo PatientInfo { get; }

        /// <summary>
        /// Yields the lines in the log files.
        /// </summary>
        /// <returns>Iterator to the lines in the log file.</returns>
        IEnumerable<ILogLine> ReadLines();
    }
}
