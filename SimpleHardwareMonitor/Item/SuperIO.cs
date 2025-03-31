using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class SuperIO : AItem<Data.SuperIO>
    {
        public SuperIO(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
