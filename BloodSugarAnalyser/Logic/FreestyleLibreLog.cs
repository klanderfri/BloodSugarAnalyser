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
        public override ExportDataType Type => ExportDataType.FreestyleLibre;

        public FreestyleLibreLog(IEnumerable<string> rawLines)
            : base(rawLines) { }

        protected override Tuple<LogLineType, ILogLine> TryGetLogLineFromRawLine(string rawLine, int lineIndex)
        {
            throw new NotImplementedException();
        }

        protected override void ExtractHeaderInformation(string rawLine, int lineIndex)
        {
            throw new NotImplementedException();
        }
    }
}
