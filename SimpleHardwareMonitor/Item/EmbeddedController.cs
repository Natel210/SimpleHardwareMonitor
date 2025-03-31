using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class EmbeddedController : AItem<Data.EmbeddedController>
    {
        public EmbeddedController(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
