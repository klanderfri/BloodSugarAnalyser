using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodSugarAnalyser.Data;
using BloodSugarAnalyser.Enums;

namespace BloodSugarAnalyser.Logic
{
    public abstract class LogLineCollection : ILogLineCollection
    {
        public abstract CgmSystem CgmSystem { get; }
        public PatientInfo PatientInfo { get; protected set; }
        private IEnumerable<string> RawLines { get; set; }
        public abstract char RawValueSeparator { get; }
        public abstract TimeSpan WarmUpPeriod { get; }
        public abstract bool HasStrictIndexOrder { get; }

        protected abstract Tuple<LogLineType, ILogLine> TryGetLogLineFromRawLine(string rawLine, int lineIndex);
        protected abstract void ExtractHeaderInformation(string rawLine, int lineIndex);
        public abstract bool AssertIndexesAreInOrder(ulong firstIndex, ulong secondIndex);

        protected LogLineCollection(IEnumerable<string> rawLines)
        {
            RawLines = rawLines;
            PatientInfo = new PatientInfo();
        }

        public IEnumerable<ILogLine> ReadLines()
        {
            int index = 0;
            foreach (var rawLine in RawLines)
            {
                var result = TryGetLogLineFromRawLine(rawLine, index);

                switch (result.Item1)
                {
                    case LogLineType.DataLine:
                        result.Item2.CheckIntegrity();
                        yield return result.Item2;
                        break;

                    case LogLineType.HeaderLine:
                        ExtractHeaderInformation(rawLine, index);
                        break;

                    default:
                        var format = "Handling not implemented for value '{0}' of enum '{1}'.";
                        var message = String.Format(format, result.Item1, nameof(LogLineType));
                        throw new NotImplementedException(message);
                }

                index++;
            }
        }

        protected string[] SplitRawLineIntoValues(string rawLine)
        {
            rawLine += Convert.ToString(RawValueSeparator);
            return rawLine.Split(RawValueSeparator);
        }

        /// <summary>
        /// Converts an input string to decimal.
        /// </summary>
        /// <param name="input">The text string to convert.</param>
        /// <returns>A decimal value.</returns>
        protected decimal? GetDecimalFromString(string input)
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
