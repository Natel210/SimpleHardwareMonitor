using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class StorageMonitor : AHardwareMonitor<StorageData>
    {
        public StorageMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void Update()
        {

        }
    }
}
