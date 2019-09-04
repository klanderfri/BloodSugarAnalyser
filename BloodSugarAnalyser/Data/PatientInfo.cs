using BloodSugarAnalyser.Logic;
using System;

namespace BloodSugarAnalyser.Data
{
    /// <summary>
    /// Class for object holding information about the patient the log belongs to.
    /// </summary>
    public class PatientInfo
    {
        /// <summary>
        /// The first name the patient.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The surname the patient.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The ID the patient.
        /// </summary>
        public int PatientID { get; set; }

        /// <summary>
        /// The date of birth of the patient.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
