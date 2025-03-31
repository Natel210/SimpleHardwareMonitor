using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Motherboard : AItem<Data.Motherboard>
    {
        public Motherboard(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
