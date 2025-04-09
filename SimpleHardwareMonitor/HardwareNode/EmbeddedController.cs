using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class EmbeddedController : AHardwareNode<Model.EmbeddedController>
    {
        internal EmbeddedController(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
        
    }
}
