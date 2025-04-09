using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Motherboard : AHardwareNode<Model.Motherboard>
    {
        internal Motherboard(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) {
            var smBios = SimpleHardwareMonitor.Instance.GetSMBios();

        }
    }
}
