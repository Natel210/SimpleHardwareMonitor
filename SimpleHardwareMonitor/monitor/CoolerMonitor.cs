using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;

namespace SimpleHardwareMonitor.monitor
{
    internal partial class CoolerMonitor : AHardwareMonitor<CoolerData>
    {
        public CoolerMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void PrevUpdate()
        {

        }
    }
}
