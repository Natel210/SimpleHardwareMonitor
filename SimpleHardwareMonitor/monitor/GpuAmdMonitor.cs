using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.common;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class GpuAmdMonitor : AHardwareMonitor<GpuAmdData>
    {
        public GpuAmdMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void PrevUpdate()
        {

        }
    }
}
