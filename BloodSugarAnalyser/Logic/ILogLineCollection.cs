using BloodSugarAnalyser.Data;
using BloodSugarAnalyser.Enums;
using System;
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
        CgmSystem CgmSystem { get; }

        /// <summary>
        /// Gets the information about the patient who the log belongs to.
        /// </summary>
        /// <remarks>The patient info will be empty if the read lines method havn't been called.</remarks>
        PatientInfo PatientInfo { get; }

        /// <summary>
        /// The time the system takes to warm up a new sensor.
        /// </summary>
        TimeSpan WarmUpPeriod { get; }

        /// <summary>
        /// Yields the lines in the log files.
        /// </summary>
        /// <returns>Iterator to the lines in the log file.</returns>
        IEnumerable<ILogLine> ReadLines();

        /// <summary>
        /// Verifies that the indexes of two lines are in order.
        /// </summary>
        /// <param name="firstIndex">The first provided index.</param>
        /// <param name="secondIndex">The second provided index.</param>
        /// <returns>TRUE if the indexes are in order, else FALSE.</returns>
        bool AssertIndexesAreInOrder(int firstIndex, int secondIndex);
    }
}
