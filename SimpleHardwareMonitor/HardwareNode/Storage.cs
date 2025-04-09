using System.Collections.Generic;
using System.Text.RegularExpressions;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Storage : AHardwareNode<Model.Storage>
    {
        public Storage(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        protected sealed override void Init()
        {
            _model.Temperature = new List<float>();
        }

        protected sealed override bool PrevUpdate()
        {
            _model.Temperature.Clear();
            return true;
        }

        protected sealed override bool CustomUpdateToSensor(ISensor sensor)
        {
            if (sensor.SensorType is SensorType.Temperature)
            {
                Regex regex = new Regex(@"temperature (25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})", RegexOptions.IgnoreCase);
                if (regex.IsMatch(sensor.Name.TrimEnd('\0').ToLower()))
                {
                    _model.Temperature.Add(sensor.Value ?? -1);
                    return true;
                }
            }
            return false;
        }

        protected sealed override void RegisterTemperatureSensorMethods()
        {
            _updateSensorMethods[SensorType.Temperature] = new Functional.SensorMethodItem() {
                { "temperature", (ISensor sensor)=>{ _model.Temperature.Add(sensor.Value ?? -1); } },
            };
        }

        protected sealed override void RegisterLoadSensorMethods()
        {
            _updateSensorMethods[SensorType.Load] = new Functional.SensorMethodItem() {
                { "used space", (ISensor sensor) => { _model.Load_Used_Space = sensor.Value ?? -1; } },
                { "read activity", (ISensor sensor) => { _model.Load_Read_Activity = sensor.Value ?? -1; } },
                { "write activity", (ISensor sensor) => { _model.Load_Write_Activity = sensor.Value ?? -1; } },
                { "total activity", (ISensor sensor) => { _model.Load_Total_Activity = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterLevelSensorMethods()
        {
            _updateSensorMethods[SensorType.Level] = new Functional.SensorMethodItem() {
                { "available spare", (ISensor sensor) => { _model.Level_Available_Spare = sensor.Value ?? -1; } },
                { "available spare threshold", (ISensor sensor) => { _model.Level_Available_Spare_Threshold = sensor.Value ?? -1; } },
                { "percentage used", (ISensor sensor) => { _model.Level_Percentage_Used = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterDataSensorMethods()
        {
            _updateSensorMethods[SensorType.Data] = new Functional.SensorMethodItem() {
                { "data read", (ISensor sensor) => { _model.Data_Read = sensor.Value ?? -1; } },
                { "data written", (ISensor sensor) => { _model.Data_Written = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterSmallDataSensorMethods()
        {
            _updateSensorMethods[SensorType.SmallData] = new Functional.SensorMethodItem() {
                { "data read", (ISensor sensor) => { _model.SmallData_Read = sensor.Value ?? -1; } },
                { "data written", (ISensor sensor) => { _model.SmallData_Written = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterThroughputSensorMethods()
        {
            _updateSensorMethods[SensorType.Throughput] = new Functional.SensorMethodItem() {
                { "read rate", (ISensor sensor) => { _model.Throughput_Read_Rate = sensor.Value ?? -1; } },
                { "write rate", (ISensor sensor) => { _model.Throughput_Write_Rate = sensor.Value ?? -1; } },
            };
        }
    }
}
