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
            : base(rawLines) { }

        public override IEnumerable<LogLine> ReadLines()
        {
            throw new NotImplementedException();
        }
    }
}
