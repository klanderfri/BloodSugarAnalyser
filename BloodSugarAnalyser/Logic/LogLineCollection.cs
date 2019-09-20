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
        public Queue<ILogLine> DataLogLines { get; private set; }

        protected abstract ILogLine TryGetLogLineFromRawLine(string rawLine, int lineIndex);
        protected abstract void ExtractHeaderInformation(string rawLine, int lineIndex);
        public abstract bool AssertIndexesAreInOrder(ulong firstIndex, ulong secondIndex);

        protected LogLineCollection(IEnumerable<string> rawLines)
        {
            RawLines = rawLines;
            PatientInfo = new PatientInfo();
            var logLines = new Queue<ILogLine>(getLogLineIterator());
            readHeaderLogLines(logLines);
            DataLogLines = logLines;
        }

        private void readHeaderLogLines(Queue<ILogLine> logLines)
        {
            if (!logLines.Any()
                || logLines.Peek().LineType != LogLineType.HeaderLine)
            {
                //No more lines at all
                //or all header lines has been read.
                return;
            }

            var line = logLines.Dequeue();
            ExtractHeaderInformation(line.RawLine, line.LineIndex);

            readHeaderLogLines(logLines);
        }

        private IEnumerable<ILogLine> getLogLineIterator()
        {
            var lineIndex = 0;
            foreach (var rawLine in RawLines)
            {
                var smartLine = TryGetLogLineFromRawLine(rawLine, lineIndex);

                switch (smartLine.LineType)
                {
                    case LogLineType.DataLine:
                        smartLine.CheckIntegrity();
                        break;

                    case LogLineType.HeaderLine:
                        //Do nothing. Just fall through and return the line.
                        break;

                    default:
                        var format = "Handling not implemented for value '{0}' of enum '{1}'.";
                        var message = String.Format(format, smartLine.LineType, nameof(LogLineType));
                        throw new NotImplementedException(message);
                }

                yield return smartLine;

                lineIndex++;
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
