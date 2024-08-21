using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.common;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class MotherboardMonitor : AHardwareMonitor<MotherboardData>
    {
        public MotherboardMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void PrevUpdate()
        {

        }
    }
}
