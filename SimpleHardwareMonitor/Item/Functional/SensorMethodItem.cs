using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item.Functional
{
    internal class SensorMethodItem : Dictionary<string, Action<ISensor>>
    {
    }
}
