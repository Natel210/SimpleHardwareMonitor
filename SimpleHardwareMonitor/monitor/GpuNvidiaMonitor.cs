using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.common;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class GpuNvidiaMonitor : AHardwareMonitor<GpuNvidiaData>
    {
        public GpuNvidiaMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void PrevUpdate()
        {

        }
    }
}
