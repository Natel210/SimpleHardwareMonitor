using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class GpuIntelMonitor : AHardwareMonitor<GpuIntelData>
    {
        public GpuIntelMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void Update()
        {

        }
    }
}
