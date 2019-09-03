using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodSugarAnalyser.Data;

namespace BloodSugarAnalyser.Logic
{
    public class DexcomClarityLog : LogLineCollection
    {
        public override ExportDataType Type => ExportDataType.DexcomClarity;

        public DexcomClarityLog(IEnumerable<string> rawLines)
            : base(rawLines.Skip(1)) { }

        protected override LogLine GetLogLineFromRawLine(string rawLine)
        {
            rawLine += ",";
            var values = rawLine.Split(',');

            var logLine = new LogLine(rawLine)
            {
                Index = Convert.ToInt32(values[0]),
                Timestamp = values[1] == "" ? (DateTime?)null : Convert.ToDateTime(values[1]),
                EventType = values[2] == "" ? null : values[2],
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

            return logLine;
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
