using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodSugarAnalyser.Data;
using BloodSugarAnalyser.Enums;

namespace BloodSugarAnalyser.Logic
{
    public class DexcomClarityLog : LogLineCollection
    {
        public override CgmSystem CgmSystem => CgmSystem.DexcomClarity;
        public override TimeSpan WarmUpPeriod => new TimeSpan(2, 0, 0);
        public override char RawValueSeparator => ',';
        public override bool HasStrictIndexOrder => true;

        public DexcomClarityLog(IEnumerable<string> rawLines)
            : base(rawLines) { }

        protected override ILogLine TryGetLogLineFromRawLine(string rawLine, int lineIndex)
        {
            var values = SplitRawLineIntoValues(rawLine);

            var logLine = new DexcomClarityLogLine(rawLine)
            {
                LineIndex = lineIndex
            };
            
            if (isDescriptionLine(values))
            {
                logLine.LineType = LogLineType.HeaderLine;
            }
            else
            {
                logLine.ID = Convert.ToUInt64(values[0]);
                logLine.Timestamp = values[1] == "" ? (DateTime?)null : Convert.ToDateTime(values[1]);
                logLine.LineType = isHeaderLine(values) ? LogLineType.HeaderLine : LogLineType.DataLine;
                logLine.DataEventType = getEventtypeFromString(values[2]);
                logLine.EventSubtype = values[3] == "" ? null : values[3];
                logLine.PatientInfo = values[4] == "" ? null : values[4];
                logLine.DeviceInfo = values[5] == "" ? null : values[5];
                logLine.SourceDeviceID = values[6] == "" ? null : values[6];
                logLine.GlucoseValue = GetDecimalFromString(values[7]);
                logLine.InsulinValue = GetDecimalFromString(values[8]);
                logLine.CarbValue = values[9] == "" ? (int?)null : Convert.ToInt32(values[9]);
                logLine.Duration = values[10] == "" ? (TimeSpan?)null : TimeSpan.Parse(values[10]);
                logLine.GlucoseRateOfChange = GetDecimalFromString(values[11]);
                logLine.TransmitterTime = values[12] == "" ? (long?)null : Convert.ToInt64(values[12]);
                logLine.TransmitterID = values[13] == "" ? null : values[13];
            }
            
            logLine.CheckIntegrity();

            return logLine;
        }

        private bool isDescriptionLine(string[] lineValues)
        {
            return lineValues[0] == "Index";
        }

        private bool isHeaderLine(string[] lineValues)
        {
            return isDescriptionLine(lineValues) || lineValues[1] == "";
        }

        protected override void ExtractHeaderInformation(string rawLine, int lineIndex)
        {
            var values = SplitRawLineIntoValues(rawLine);

            switch (values[2])
            {
                case "FirstName":
                    PatientInfo.FirstName = values[4];
                    break;
                case "LastName":
                    PatientInfo.Surname = values[4];
                    break;
                case "PatientIdentifier":
                    PatientInfo.PatientID = Convert.ToInt32(values[4]);
                    break;
                case "DateOfBirth":
                    PatientInfo.DateOfBirth = Convert.ToDateTime(values[4]);
                    break;
            }
        }

        private LogEventType getEventtypeFromString(string rawData)
        {
            switch (rawData)
            {
                case "EGV":
                    return LogEventType.GlucoseMeasurement;
                case "Calibration":
                    return LogEventType.Calibration;
                default:
                    return LogEventType.Unknown;
            }
        }

        /// <summary>
        /// Verifies that two indexes are in order.
        /// </summary>
        /// <param name="firstIndex">The first provided index.</param>
        /// <param name="secondIndex">The second provided index.</param>
        /// <returns>TRUE if the indexes are in order, else FALSE.</returns>
        public override bool AssertIndexesAreInOrder(ulong firstIndex, ulong secondIndex)
        {
            return firstIndex < secondIndex;
        }
    }
}
