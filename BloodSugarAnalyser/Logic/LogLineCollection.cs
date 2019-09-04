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
        public abstract ExportDataType Type { get; }
        public PatientInfo PatientInfo { get; protected set; }
        private IEnumerable<string> RawLines { get; set; }

        protected abstract Tuple<LogLineType, ILogLine> TryGetLogLineFromRawLine(string rawLine, int lineIndex);
        protected abstract void ExtractHeaderInformation(string rawLine, int lineIndex);

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
    }
}
