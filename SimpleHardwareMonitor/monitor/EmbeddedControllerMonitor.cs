using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.common;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class EmbeddedControllerMonitor : AHardwareMonitor<EmbeddedControllerData>
    {
        public EmbeddedControllerMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void PrevUpdate()
        {

        }
    }
}
