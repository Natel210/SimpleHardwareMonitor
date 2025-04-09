using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Cooler : AHardwareNode<Model.Cooler>
    {
        internal Cooler(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
