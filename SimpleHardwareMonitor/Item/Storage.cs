using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.Item.Functional;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Storage : AItem<Data.Storage>
    {
        protected sealed override void Init()
        {
            _data.Temperature = new List<float>();
            FillSensorMethods();
        }

        protected sealed override bool PrevUpdate()
        {
            _data.Temperature.Clear();
            //


            return true;
        }

        public Storage(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        private void FillSensorMethods()
        {
            _updateSensorMethods.Clear();

            // Temperature
            _updateSensorMethods[SensorType.Load] = new SensorMethodItem() {
                { "temperature", (ISensor sensor)=>{ _data.Temperature.Add(sensor.Value ?? -1); } },
            };

            // Load
            _updateSensorMethods[SensorType.Load] = new SensorMethodItem() {
                { "used space", (ISensor sensor) => { _data.Load_Used_Space = sensor.Value ?? -1; } },
                { "read activity", (ISensor sensor) => { _data.Load_Read_Activity = sensor.Value ?? -1; } },
                { "write activity", (ISensor sensor) => { _data.Load_Write_Activity = sensor.Value ?? -1; } },
                { "total activity", (ISensor sensor) => { _data.Load_Total_Activity = sensor.Value ?? -1; } },
            };

            //// Level
            //_updateSensorMethods[SensorType.Data] = new SensorMethodItem() {
            //    { "memory used", (ISensor sensor) => { _data.Data_Used = sensor.Value ?? -1; } },
            //    { "memory available", (ISensor sensor) => { _data.Data_Available = sensor.Value ?? -1; } },
            //    { "virtual memory used", (ISensor sensor) => { _data.Data_Virtual_Used = sensor.Value ?? -1; } },
            //    { "virtual memory available", (ISensor sensor) => { _data.Data_Virtual_Available = sensor.Value ?? -1; } },
            //};

            //// Data
            //_updateSensorMethods[SensorType.Data] = new SensorMethodItem() {
            //    { "memory used", (ISensor sensor) => { _data.Data_Used = sensor.Value ?? -1; } },
            //    { "memory available", (ISensor sensor) => { _data.Data_Available = sensor.Value ?? -1; } },
            //    { "virtual memory used", (ISensor sensor) => { _data.Data_Virtual_Used = sensor.Value ?? -1; } },
            //    { "virtual memory available", (ISensor sensor) => { _data.Data_Virtual_Available = sensor.Value ?? -1; } },
            //};

            //// Data
            //_updateSensorMethods[SensorType.SmallData] = new SensorMethodItem() {
            //    { "memory used", (ISensor sensor) => { _data.SmallData_Used = sensor.Value ?? -1; } },
            //    { "memory available", (ISensor sensor) => { _data.SmallData_Available = sensor.Value ?? -1; } },
            //    { "virtual memory used", (ISensor sensor) => { _data.SmallData_Virtual_Used = sensor.Value ?? -1; } },
            //    { "virtual memory available", (ISensor sensor) => { _data.SmallData_Virtual_Available = sensor.Value ?? -1; } },
            //};
        }
    }
}
