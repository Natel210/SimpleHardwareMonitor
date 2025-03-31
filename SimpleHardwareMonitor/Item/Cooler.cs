using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Cooler : AItem<Data.Cooler>
    {
        public Cooler(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
