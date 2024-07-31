using SimpleHardwareMonitor.@base;
using System.IO;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class CoolerViewmodel : AHardwareMonitorViewmodel
    {

    }

    public partial class CoolerViewmodel : AHardwareMonitorViewmodel
    {
        public CoolerViewmodel(SynchronizationContext syncContext) : base(syncContext) { }
        protected override bool UpdateData_Inner() { return true; }
    }
}
