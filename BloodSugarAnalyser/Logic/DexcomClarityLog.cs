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
        public override ExportDataType Type => ExportDataType.DexcomClarity;

        public DexcomClarityLog(IEnumerable<string> rawLines)
            : base(rawLines) { }

        protected override Tuple<LogLineType, ILogLine> TryGetLogLineFromRawLine(string rawLine, int lineIndex)
        {
            var values = getLineValues(rawLine);

            if (isHeaderLine(values))
            {
                return new Tuple<LogLineType, ILogLine>(LogLineType.HeaderLine, null);
            }
            else
            {
                var logLine = new DexcomClarityLogLine(rawLine)
                {
                    Index = Convert.ToInt32(values[0]),
                    Timestamp = values[1] == "" ? (DateTime?)null : Convert.ToDateTime(values[1]),
                    EventType = getEventtypeFromString(values[2]),
                    EventSubtype = values[3] == "" ? null : values[3],
                    PatientInfo = values[4] == "" ? null : values[4],
                    DeviceInfo = values[5] == "" ? null : values[5],
                    SourceDeviceID = values[6] == "" ? null : values[6],
                    GlucoseValue = convertToDecimal(values[7]),
                    InsulinValue = convertToDecimal(values[8]),
                    CarbValue = values[9] == "" ? (int?)null : Convert.ToInt32(values[9]),
                    Duration = values[10] == "" ? (TimeSpan?)null : TimeSpan.Parse(values[10]),
                    GlucoseRateOfChange = convertToDecimal(values[11]),
                    TransmitterTime = values[12] == "" ? (long?)null : Convert.ToInt64(values[12]),
                    TransmitterID = values[13] == "" ? null : values[13],
                };

                return new Tuple<LogLineType, ILogLine>(LogLineType.DataLine, logLine);
            }
        }

        private static string[] getLineValues(string rawLine)
        {
            rawLine += ",";
            return rawLine.Split(',');
        }

        private bool isHeaderLine(string[] lineValues)
        {
            return lineValues[0] == "Index" || lineValues[1] == "";
        }

        protected override void ExtractHeaderInformation(string rawLine, int lineIndex)
        {
            var values = getLineValues(rawLine);

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
        /// Converts a Clarity input string to decimal.
        /// </summary>
        /// <param name="input">The text string to convert.</param>
        /// <returns>A decimal value.</returns>
        private decimal? convertToDecimal(string input)
        {
            if (input == "")
            {
                return null;
            }
            else
            {
                input = input
                    .Replace("Low", "0")
                    .Replace('.', ',');
                return Convert.ToDecimal(input);
            }
        }
    }
}
