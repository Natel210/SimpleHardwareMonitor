using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Network : AItem<Data.Network>
    {
        public Network(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
