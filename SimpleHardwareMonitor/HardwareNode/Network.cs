using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Network : AHardwareNode<Model.Network>
    {
        internal Network(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
