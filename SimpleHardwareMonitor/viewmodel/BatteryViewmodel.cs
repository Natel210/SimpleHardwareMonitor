using SimpleHardwareMonitor.@base;
using System.IO;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class BatteryViewmodel : AHardwareMonitorViewmodel
    {

    }

    public partial class BatteryViewmodel : AHardwareMonitorViewmodel
    {



        public BatteryViewmodel(SynchronizationContext syncContext) : base(syncContext) { }
        protected override bool UpdateData_Inner() { return true; }
    }
}
