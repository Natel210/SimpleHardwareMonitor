using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Items
{
    public class LibreHardWareMonitorWrapper : IInfoItem
    {
        public LibreHardWareMonitorWrapper(Computer computer)
        {
            _computer = computer;
        }

        private readonly Computer _computer = null;
        HardwareType _hardwareType;
        string _sensorName;

        public void Start() { }
        public void Stop() { }
        public void Restart() { }

        public int GetUpdateInterval() { return 0; }
        public void SetUpdateInterval(int interval) { }

        public double GetValue() { return 0; }

        private async Task UpdateValueAsync()
        {
            var cpuHardware = _computer.Hardware.FirstOrDefault(
                hw => hw.HardwareType == HardwareType.Cpu);
            cpuHardware.Update();
            var checkSensorHardware = cpuHardware.Sensors.FirstOrDefault(
                s => s.Name == _sensorName);
            checkSensorHardware.Value;


            while (true) {




            }

    }
}
