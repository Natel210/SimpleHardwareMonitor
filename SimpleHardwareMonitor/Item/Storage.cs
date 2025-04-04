using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.Item.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            return true;
        }

        protected sealed override bool CustomUpdateToSensor(ISensor sensor)
        {
            if (sensor.SensorType is SensorType.Temperature)
            {
                Regex regex = new Regex(@"temperature (25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})", RegexOptions.IgnoreCase);
                if (regex.IsMatch(sensor.Name.TrimEnd('\0').ToLower()))
                {
                    _data.Temperature.Add(sensor.Value ?? -1);
                    return true;
                }
                    
            }
            return false;
        }

        public Storage(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        private void FillSensorMethods()
        {
            _updateSensorMethods.Clear();

            /*---- [ Voltage ] ---------------------------------------------------*/
            /*---- [ Current ] ---------------------------------------------------*/
            /*---- [ Power ] -----------------------------------------------------*/
            /*---- [ Clock ] -----------------------------------------------------*/
            /*---- [ Temperature ] -----------------------------------------------*/
            _updateSensorMethods[SensorType.Temperature] = new SensorMethodItem() {
                { "temperature", (ISensor sensor)=>{ _data.Temperature.Add(sensor.Value ?? -1); } },
            };
            /*---- [ Load ] ------------------------------------------------------*/
            _updateSensorMethods[SensorType.Load] = new SensorMethodItem() {
                { "used space", (ISensor sensor) => { _data.Load_Used_Space = sensor.Value ?? -1; } },
                { "read activity", (ISensor sensor) => { _data.Load_Read_Activity = sensor.Value ?? -1; } },
                { "write activity", (ISensor sensor) => { _data.Load_Write_Activity = sensor.Value ?? -1; } },
                { "total activity", (ISensor sensor) => { _data.Load_Total_Activity = sensor.Value ?? -1; } },
            };
            /*---- [ Frequency ] -------------------------------------------------*/
            /*---- [ Fan ] -------------------------------------------------------*/
            /*---- [ Flow ] ------------------------------------------------------*/
            /*---- [ Control ] ---------------------------------------------------*/
            /*---- [ Level ] -----------------------------------------------------*/
            _updateSensorMethods[SensorType.Level] = new SensorMethodItem() {
                { "available spare", (ISensor sensor) => { _data.Level_Available_Spare = sensor.Value ?? -1; } },
                { "available spare threshold", (ISensor sensor) => { _data.Level_Available_Spare_Threshold = sensor.Value ?? -1; } },
                { "percentage used", (ISensor sensor) => { _data.Level_Percentage_Used = sensor.Value ?? -1; } },
            };
            /*---- [ Data ] ------------------------------------------------------*/
            _updateSensorMethods[SensorType.Data] = new SensorMethodItem() {
                { "data read", (ISensor sensor) => { _data.Data_Read = sensor.Value ?? -1; } },
                { "data written", (ISensor sensor) => { _data.Data_Written = sensor.Value ?? -1; } },
            };
            /*---- [ Small Data ] ------------------------------------------------*/
            _updateSensorMethods[SensorType.SmallData] = new SensorMethodItem() {
                { "data read", (ISensor sensor) => { _data.SmallData_Read = sensor.Value ?? -1; } },
                { "data written", (ISensor sensor) => { _data.SmallData_Written = sensor.Value ?? -1; } },
            };
            /*---- [ Throughput ] ------------------------------------------------*/
            _updateSensorMethods[SensorType.Throughput] = new SensorMethodItem() {
                { "read rate", (ISensor sensor) => { _data.Throughput_Read_Rate = sensor.Value ?? -1; } },
                { "write rate", (ISensor sensor) => { _data.Throughput_Write_Rate = sensor.Value ?? -1; } },
            };
            /*---- [ Time Span ] -------------------------------------------------*/
            /*---- [ Energy ] ----------------------------------------------------*/
            /*---- [ Noise ] -----------------------------------------------------*/

        }
    }
}
