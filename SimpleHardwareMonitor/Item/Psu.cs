using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Psu : AItem<Data.Psu>
    {
        public Psu(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
