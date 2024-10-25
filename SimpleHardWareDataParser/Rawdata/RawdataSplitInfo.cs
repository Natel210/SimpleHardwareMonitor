using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardWareDataParser.Rawdata
{
    internal class RawdataSplitInfo
    {
        internal string SplitName { get; set; } = "";
        internal DateTime SplitStart { get; set; }
        internal DateTime SplitEnd { get; set; }
    }
}
