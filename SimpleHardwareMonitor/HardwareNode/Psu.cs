using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Psu : AHardwareNode<Model.Psu>
    {
        internal Psu(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
