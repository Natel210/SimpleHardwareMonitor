using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.Item.Functional;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace SimpleHardwareMonitor.Item
{
    internal partial class Cpu : AItem<Data.Cpu>
    {
        private PerformanceCounter _usePerformance;
        private List<PerformanceCounter> _usePerformanceByThreads;

        protected sealed override void Init()
        {
            ManagementObjectSearcher searcherName = new ManagementObjectSearcher("select Name from Win32_Processor");
            foreach (ManagementObject obj in searcherName.Get())
            {
                _data.Name = obj["Name"].ToString();
                break;
            }
            _data.CoreCount = GetPhysicalCoreCount();
            _data.ProcessorCount = Environment.ProcessorCount;
            _usePerformance = new PerformanceCounter("Processor Information", "% Processor Utility", "0,_Total", true);
            _data.Use_ByThreads = new List<float>();
            _usePerformanceByThreads = new List<PerformanceCounter>();
            for (int i = 0; i < Environment.ProcessorCount; ++i)
            {
                _usePerformanceByThreads.Add(new PerformanceCounter("Processor Information", "% Processor Utility", $"0,{i}"));
            }

            FillZreoList();
            FillSensorMethods();
        }

        protected sealed override bool PrevUpdate()
        {
            //reset 0f
            FillZreoList();

            _data.Use = _usePerformance.NextValue();
            for (int i = 0; i < Environment.ProcessorCount; ++i)
                _data.Use_ByThreads[i] = _usePerformanceByThreads[i].NextValue();
            return true;
        }

        internal Cpu(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        private void FillZreoList()
        {
            _data.Use_ByThreads = Enumerable.Repeat(0f, _data.ProcessorCount).ToList();
            _data.Load_ByThreads = Enumerable.Repeat(0f, _data.ProcessorCount).ToList();
            _data.Temperature_ByCore = Enumerable.Repeat(0f, _data.CoreCount).ToList();
            _data.Temperature_Distanceto_Tj_Max_ByCore = Enumerable.Repeat(0f, _data.CoreCount).ToList();
            _data.Clock_ByCore = Enumerable.Repeat(0f, _data.CoreCount).ToList();
            _data.Voltage_ByCore = Enumerable.Repeat(0f, _data.CoreCount).ToList();
        }

        private void FillSensorMethods()
        {
            _updateSensorMethods.Clear();

            // Voltage
            var voltageMethodItem = new SensorMethodItem();
            voltageMethodItem.Add("cpu core", (ISensor sensor) => { _data.Voltage = sensor.Value ?? -1; });
            for (int index = 0; index < _data.CoreCount; ++index)
            {
                // The index is registered in a modified state.
                int temp = index;
                int tempNum = index + 1;
                voltageMethodItem.Add($"cpu core #{tempNum}", (ISensor sensor) => { _data.Voltage_ByCore[temp] = sensor.Value ?? -1; });
            }
            _updateSensorMethods[SensorType.Voltage] = voltageMethodItem;

            // Power
            _updateSensorMethods[SensorType.Power] = new SensorMethodItem() {
                { "cpu package", (ISensor sensor) => { _data.Power_Package = sensor.Value ?? -1; } },
                { "cpu cores", (ISensor sensor) => { _data.Power_Cores = sensor.Value ?? -1; } },
                { "cpu memory", (ISensor sensor) => { _data.Power_Memory = sensor.Value ?? -1; } },
                { "cpu platform", (ISensor sensor) => { _data.Power_Platform = sensor.Value ?? -1; } },
            };

            // Clock
            var clockMethodItem = new SensorMethodItem();
            clockMethodItem.Add("bus speed", (ISensor sensor) => { _data.Clock_Bus_Speed = sensor.Value ?? -1; });
            for (int index = 0; index < _data.CoreCount; ++index)
            {
                // The index is registered in a modified state.
                int temp = index;
                int tempNum = index + 1;
                clockMethodItem.Add($"cpu core #{tempNum}", (ISensor sensor) => { _data.Clock_ByCore[temp] = sensor.Value ?? -1; });
            }
            _updateSensorMethods[SensorType.Clock] = clockMethodItem;

            // Temperature
            var temperatureMethodItem = new SensorMethodItem();
            temperatureMethodItem.Add("cpu package", (ISensor sensor) => { _data.Temperature_Package = sensor.Value ?? -1; });
            temperatureMethodItem.Add("core max", (ISensor sensor) => { _data.Temperature_Max = sensor.Value ?? -1; });
            temperatureMethodItem.Add("core average", (ISensor sensor) => { _data.Temperature_Average = sensor.Value ?? -1; });
            for (int index = 0; index < _data.CoreCount; ++index)
            {
                // The index is registered in a modified state.
                int temp = index;
                int tempNum = index + 1;
                temperatureMethodItem.Add($"cpu core #{tempNum}", (ISensor sensor) => { _data.Temperature_ByCore[temp] = sensor.Value ?? -1; });
            }
            for (int index = 0; index < _data.CoreCount; ++index)
            {
                // The index is registered in a modified state.
                int temp = index;
                int tempNum = index + 1;
                temperatureMethodItem.Add($"cpu core #{tempNum} distance to tjmax", (ISensor sensor) => { _data.Temperature_Distanceto_Tj_Max_ByCore[temp] = sensor.Value ?? -1; });
            }
            _updateSensorMethods[SensorType.Temperature] = temperatureMethodItem;

            // Load
            var loadMethodItem = new SensorMethodItem();
            loadMethodItem.Add("cpu total", (ISensor sensor) => { _data.Load_Total = sensor.Value ?? -1; });
            loadMethodItem.Add("cpu core max", (ISensor sensor) => { _data.Load_Max = sensor.Value ?? -1; });
            for (int index = 0; index < _data.CoreCount; ++index)
            {
                int temp = index;
                int tempNum = index + 1;
                int tempMul2 = index * 2;
                loadMethodItem.Add($"cpu core #{tempNum} thread #1", (ISensor sensor) => { _data.Load_ByThreads[tempMul2] = sensor.Value ?? -1; });
                loadMethodItem.Add($"cpu core #{tempNum} thread #2", (ISensor sensor) => { _data.Load_ByThreads[(tempMul2) + 1] = sensor.Value ?? -1; });
            }
            _updateSensorMethods[SensorType.Load] = loadMethodItem;
        }

        private int GetPhysicalCoreCount()
        {
            int coreCount = 0;
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select NumberOfCores from Win32_Processor"))
                {
                    foreach (ManagementObject item in searcher.Get())
                        coreCount += int.Parse(item["NumberOfCores"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching physical core count: {ex.Message}", ex);
            }
            return coreCount;
        }
    }
}
