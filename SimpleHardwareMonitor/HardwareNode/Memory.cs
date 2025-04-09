using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Memory : AHardwareNode<Model.Memory>
    {
        internal Memory(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        protected sealed override void RegisterLoadSensorMethods()
        {
            _updateSensorMethods[SensorType.Load] = new Functional.SensorMethodItem() {
                { "memory", (ISensor sensor)=>{ _model.Load_Memory = sensor.Value ?? -1; } },
                { "virtual memory", (ISensor sensor)=>{ _model.Load_Virtual_Memory = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterDataSensorMethods()
        {
            _updateSensorMethods[SensorType.Data] = new Functional.SensorMethodItem() {
                { "memory used", (ISensor sensor) => { _model.Data_Used = sensor.Value ?? -1; } },
                { "memory available", (ISensor sensor) => { _model.Data_Available = sensor.Value ?? -1; } },
                { "virtual memory used", (ISensor sensor) => { _model.Data_Virtual_Used = sensor.Value ?? -1; } },
                { "virtual memory available", (ISensor sensor) => { _model.Data_Virtual_Available = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterSmallDataSensorMethods()
        {
            _updateSensorMethods[SensorType.SmallData] = new Functional.SensorMethodItem() {
                { "memory used", (ISensor sensor) => { _model.SmallData_Used = sensor.Value ?? -1; } },
                { "memory available", (ISensor sensor) => { _model.SmallData_Available = sensor.Value ?? -1; } },
                { "virtual memory used", (ISensor sensor) => { _model.SmallData_Virtual_Used = sensor.Value ?? -1; } },
                { "virtual memory available", (ISensor sensor) => { _model.SmallData_Virtual_Available = sensor.Value ?? -1; } },
            };
        }
    }
}
