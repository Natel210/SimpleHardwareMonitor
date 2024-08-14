using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger.UserProperties
{
    public struct LoggerProperties
    {
        public DirectoryInfo RootDirectory { get; set;}
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}
