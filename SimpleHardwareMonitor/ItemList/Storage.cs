using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Storage : AItemList<Data.Storage, Item.Storage>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Storage(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Storage> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Storage>();
            foreach (var item in _item)
            {
                var tempData = new Data.Storage();
                var getData = item.Value.Data;

                // Common
                tempData.Name = item.Key;

                // Temperature 
                tempData.Temperature = new List<float>(getData.Temperature);

                // Load
                tempData.Load_Used_Space = getData.Load_Used_Space;
                tempData.Load_Read_Activity = getData.Load_Read_Activity;
                tempData.Load_Write_Activity = getData.Load_Write_Activity;
                tempData.Load_Total_Activity = getData.Load_Total_Activity;

                // Level
                tempData.Level_Available_Spare = getData.Level_Available_Spare;
                tempData.Level_Available_Spare_Threshold = getData.Level_Available_Spare_Threshold;
                tempData.Level_Percentage_Used = getData.Level_Percentage_Used;

                // Data
                tempData.Data_Read = getData.Data_Read;
                tempData.Data_Written = getData.Data_Written;

                // Small Data
                tempData.SmallData_Read = getData.SmallData_Read;
                tempData.SmallData_Written = getData.SmallData_Written;

                // Throughput
                tempData.Throughput_Read_Rate = getData.Throughput_Read_Rate;
                tempData.Throughput_Write_Rate = getData.Throughput_Write_Rate;

                dataList.Add(item.Key, tempData);
            }
            return dataList;
        }
    }
}
