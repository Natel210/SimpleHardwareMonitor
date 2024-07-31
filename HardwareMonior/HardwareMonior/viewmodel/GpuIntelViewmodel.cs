using SimpleHardwareMonitor.@base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class GpuIntelViewmodel : AHardwareMonitorViewmodel
    {

    }

    public partial class GpuIntelViewmodel : AHardwareMonitorViewmodel
    {
        public GpuIntelViewmodel(SynchronizationContext syncContext) : base(syncContext) { }
        protected override bool UpdateData_Inner() { return true; }
    }
}
