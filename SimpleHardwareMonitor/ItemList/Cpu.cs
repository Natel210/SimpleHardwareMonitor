using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Cpu : AItemList<Data.Cpu, Item.Cpu>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Cpu(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Cpu> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Cpu>();
            foreach (var item in _item)
            {
                var tempData = new Data.Cpu();
                var getData = item.Value.Data;

                // Common
                tempData.Name = item.Key;
                tempData.CoreCount = getData.CoreCount;
                tempData.ProcessorCount = getData.ProcessorCount;

                // Use
                tempData.Use = getData.Use;
                tempData.Use_ByThreads = new List<float>(getData.Use_ByThreads);

                // Voltage
                tempData.Voltage = getData.Voltage;
                tempData.Voltage_ByCore = new List<float>(getData.Voltage_ByCore);

                // Power
                tempData.Power_Package = getData.Power_Package;
                tempData.Power_Cores = getData.Power_Cores;
                tempData.Power_Memory = getData.Power_Memory;
                tempData.Power_Platform = getData.Power_Platform;

                // Clock
                tempData.Clock_Bus_Speed = getData.Clock_Bus_Speed;
                tempData.Clock_ByCore = new List<float>(getData.Clock_ByCore);

                // Temperature
                tempData.Temperature_Package = getData.Temperature_Package;
                tempData.Temperature_Max = getData.Temperature_Max;
                tempData.Temperature_Average = getData.Temperature_Average;
                tempData.Temperature_ByCore = new List<float>(getData.Temperature_ByCore);
                tempData.Temperature_Distanceto_Tj_Max_ByCore = new List<float>(getData.Temperature_Distanceto_Tj_Max_ByCore);

                // Load
                tempData.Load_Total = getData.Load_Total;
                tempData.Load_Max = getData.Load_Max;
                tempData.Load_ByThreads = new List<float>(getData.Load_ByThreads);
                dataList.Add(item.Key, tempData);
            }
            return dataList;
        }
    }
}
