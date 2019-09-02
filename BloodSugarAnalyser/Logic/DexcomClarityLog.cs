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
            : base(rawLines) { }

        public override IEnumerable<LogLine> ReadLines()
        {
            foreach (var rawLine in RawLines.Skip(1))
            {
                yield return new LogLine(rawLine);
            }
        }
    }
}
