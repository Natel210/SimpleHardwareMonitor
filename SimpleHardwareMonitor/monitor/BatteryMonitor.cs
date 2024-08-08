using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class BatteryMonitor : AHardwareMonitor<BatteryData>
    {
        public BatteryMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void PrevUpdate()
        {

        }
    }
}
