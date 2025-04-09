using System;
using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode.Functional
{
    internal class SensorMethodItem : Dictionary<string, Action<ISensor>>
    {
    }
}
