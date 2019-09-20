using BloodSugarAnalyser.Data;
using BloodSugarAnalyser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodSugarAnalyser.Logic
{
    public class FreestyleLibreLog : LogLineCollection
    {
        public override CgmSystem CgmSystem => CgmSystem.FreestyleLibre;
        public override TimeSpan WarmUpPeriod => new TimeSpan(1, 0, 0);
        public override char RawValueSeparator => '\t';
        public override bool HasStrictIndexOrder => false;

        public FreestyleLibreLog(IEnumerable<string> rawLines)
            : base(rawLines) { }

        protected override ILogLine TryGetLogLineFromRawLine(string rawLine, int lineIndex)
        {
            var values = SplitRawLineIntoValues(rawLine);
            var rowStartsWithInteger = isNumeric(values[0]);

            var logLine = new FreestyleLibreLogLine(rawLine)
            {
                ID = Convert.ToUInt64(values[0]),
                LineIndex = lineIndex,
                Timestamp = values[1] == "" ? (DateTime?)null : Convert.ToDateTime(values[1]),
                LineType = rowStartsWithInteger ? LogLineType.DataLine : LogLineType.HeaderLine,
                GlucoseValue = GetDecimalFromString(values[3]) ?? GetDecimalFromString(values[4]) ?? GetDecimalFromString(values[12]),
                DataEventType = getEventtypeFromData(values[3], values[4], values[12]),
            };
            logLine.CheckIntegrity();

            return logLine;
        }

        private LogEventType getEventtypeFromData(string historicGlucose, string readGlucose, string testedGlucose)
        {
            if (isNumeric(historicGlucose) || isNumeric(readGlucose))
            {
                return LogEventType.GlucoseMeasurement;
            }
            else if (isNumeric(testedGlucose))
            {
                return LogEventType.Calibration;
            }
            else
            {
                return LogEventType.Unknown;
            }
        }

        private bool isNumeric(string data)
        {
            return !String.IsNullOrWhiteSpace(data) && decimal.TryParse(data, out decimal _);
        }

        protected override void ExtractHeaderInformation(string rawLine, int lineIndex)
        {
            if (rawLine.StartsWith("#"))
            {
                PatientInfo.PatientID = Convert.ToInt32(rawLine.Substring(2));
            }
            else if (lineIndex == 0)
            {
                var surnameStartIndex = rawLine.LastIndexOf(' ');

                if (surnameStartIndex < 0)
                {
                    PatientInfo.Surname = rawLine;
                }
                else
                {
                    PatientInfo.FirstName = rawLine.Substring(0, surnameStartIndex);
                    PatientInfo.Surname = rawLine.Substring(surnameStartIndex + 1);
                }
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
            return firstIndex <= secondIndex;
        }
    }
}
