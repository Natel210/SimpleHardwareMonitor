using SimpleHardWareDataParser.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardWareDataParser.Rawdata
{
    internal class RawdataRecordViewmodel : AViewModelBase_None
    {
        public ObservableCollection<RawdataItem> DataRecords { get; set; } = new();
    }
}
