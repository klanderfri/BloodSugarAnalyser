using System;

namespace BloodSugarAnalyser
{
    class PatientInfo
    {
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public int PatientID { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        
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
