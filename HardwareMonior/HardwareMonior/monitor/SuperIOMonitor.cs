using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class SuperIOMonitor : AHardwareMonitor<SuperIOData>
    {
        public SuperIOMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void Update()
        {

        }
    }
}
