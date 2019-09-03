using BloodSugarAnalyser.Data;
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
            : base(rawLines.Skip(3)) { }

        protected override LogLine GetLogLineFromRawLine(string rawLine)
        {
            throw new NotImplementedException();
        }
    }
}
