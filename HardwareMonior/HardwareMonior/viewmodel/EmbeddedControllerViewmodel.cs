using SimpleHardwareMonitor.@base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class EmbeddedControllerViewmodel : AHardwareMonitorViewmodel
    {

    }

    public partial class EmbeddedControllerViewmodel : AHardwareMonitorViewmodel
    {
        public EmbeddedControllerViewmodel(SynchronizationContext syncContext) : base(syncContext) { }
        protected override bool UpdateData_Inner() { return true; }
    }
}
