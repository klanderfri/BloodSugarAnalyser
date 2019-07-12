using System;

namespace BloodSugarAnalyser
{
    /// <summary>
    /// Class for object holding information about the patient the log belongs to.
    /// </summary>
    class PatientInfo
    {
        /// <summary>
        /// The first name the patient.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// The surname the patient.
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// The ID the patient.
        /// </summary>
        public int PatientID { get; private set; }

        /// <summary>
        /// The date of birth of the patient.
        /// </summary>
        public DateTime DateOfBirth { get; private set; }
        
        /// <summary>
        /// Stores the patient info from a log line.
        /// </summary>
        /// <param name="logLine">The log line to store patient info from.</param>
        public void StoreInfo(ClarityLogLine logLine)
        {
            if (logLine.EventType == "FirstName")
            {
                FirstName = logLine.PatientInfo;
            }
            if (logLine.EventType == "LastName")
            {
                Surname = logLine.PatientInfo;
            }
            if (logLine.EventType == "PatientIdentifier")
            {
                PatientID = Convert.ToInt32(logLine.PatientInfo);
            }
            if (logLine.EventType == "DateOfBirth")
            {
                DateOfBirth = Convert.ToDateTime(logLine.PatientInfo);
            }
        }
    }
}
