using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodSugarAnalyser.Enums
{
    /// <summary>
    /// The type of event that caused the creation of a log line.
    /// </summary>
    public enum LogEventType
    {
        Unknown,
        GlucoseMeasurement,
        Calibration
    }
}
