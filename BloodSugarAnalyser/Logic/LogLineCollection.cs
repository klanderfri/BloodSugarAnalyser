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
        protected IEnumerable<string> RawLines { get; set; }

        public abstract ExportDataType Type { get; }

        public abstract IEnumerable<LogLine> ReadLines();

        protected LogLineCollection(IEnumerable<string> rawLines)
        {
            RawLines = rawLines;
        }
    }
}
