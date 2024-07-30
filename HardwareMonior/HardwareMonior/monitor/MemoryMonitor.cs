using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class MemoryMonitor : AHardwareMonitor<MemoryData>
    {
        public MemoryMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void Update()
        {

        }
    }
}
