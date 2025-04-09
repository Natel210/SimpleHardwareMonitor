using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class SuperIO : AHardwareNode<Model.SuperIO>
    {
        public SuperIO(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
