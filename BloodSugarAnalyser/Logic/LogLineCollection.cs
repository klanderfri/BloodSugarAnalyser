using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodSugarAnalyser.Data;

namespace BloodSugarAnalyser.Logic
{
    public abstract class LogLineCollection : ILogLineCollection
    {
        private IEnumerable<string> RawLines { get; set; }
        public abstract ExportDataType Type { get; }

        protected abstract LogLine GetLogLineFromRawLine(string rawLine);

        protected LogLineCollection(IEnumerable<string> rawLines)
        {
            RawLines = rawLines;
        }

        public IEnumerable<LogLine> ReadLines()
        {
            foreach (var rawLine in RawLines)
            {
                yield return GetLogLineFromRawLine(rawLine);
            }
        }
    }
}
