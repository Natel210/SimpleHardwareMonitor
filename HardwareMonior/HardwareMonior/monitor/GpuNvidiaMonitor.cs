using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class GpuNvidiaMonitor : AHardwareMonitor<GpuNvidiaData>
    {
        public GpuNvidiaMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void Update()
        {

        }
    }
}
