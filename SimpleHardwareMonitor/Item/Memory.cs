using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.Item.Functional;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Memory : AItem<Data.Memory>
    {
        protected sealed override void Init()
        {
            FillSensorMethods();
        }

        internal Memory(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        private void FillSensorMethods()
        {
            _updateSensorMethods.Clear();

            // Load
            _updateSensorMethods[SensorType.Load] = new SensorMethodItem() {
                { "d3d 3d", (ISensor sensor)=>{ _data.ToString.Add(sensor.Value ?? -1); } },
            };

            // Data
            _updateSensorMethods[SensorType.Data] = new SensorMethodItem() {
                { "memory used", (ISensor sensor) => { _data.Data_Used = sensor.Value ?? -1; } },
                { "memory available", (ISensor sensor) => { _data.Data_Available = sensor.Value ?? -1; } },
                { "virtual memory used", (ISensor sensor) => { _data.Data_Virtual_Used = sensor.Value ?? -1; } },
                { "virtual memory available", (ISensor sensor) => { _data.Data_Virtual_Available = sensor.Value ?? -1; } },
            };

            // Data
            _updateSensorMethods[SensorType.SmallData] = new SensorMethodItem() {
                { "memory used", (ISensor sensor) => { _data.SmallData_Used = sensor.Value ?? -1; } },
                { "memory available", (ISensor sensor) => { _data.SmallData_Available = sensor.Value ?? -1; } },
                { "virtual memory used", (ISensor sensor) => { _data.SmallData_Virtual_Used = sensor.Value ?? -1; } },
                { "virtual memory available", (ISensor sensor) => { _data.SmallData_Virtual_Available = sensor.Value ?? -1; } },
            };
        }
    }
}
