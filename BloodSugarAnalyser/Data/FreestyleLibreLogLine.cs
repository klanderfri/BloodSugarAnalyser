using BloodSugarAnalyser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodSugarAnalyser.Data
{
    public class FreestyleLibreLogLine : LogLine
    {
        public FreestyleLibreLogLine(string rawLine)
            : base(rawLine) { }
    }
}
