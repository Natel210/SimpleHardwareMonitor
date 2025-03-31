using LibreHardwareMonitor.Hardware;
namespace SimpleHardwareMonitor.Item
{
    internal partial class Battery : AItem<Data.Battery>
    {
        public Battery(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }
    }
}
