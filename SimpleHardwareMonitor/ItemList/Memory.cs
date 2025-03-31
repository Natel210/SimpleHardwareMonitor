using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Memory : AItemList<Data.Memory, Item.Memory>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Memory(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Memory> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Memory>();
            foreach (var item in _item)
            {
                var tempData = new Data.Memory();
                var getData = item.Value.Data;
                // Common
                tempData.Name = item.Key;

                // Data
                tempData.Data_Used = getData.Data_Used;
                tempData.Data_Available = getData.Data_Available;
                tempData.Data_Virtual_Used = getData.Data_Virtual_Used;
                tempData.Data_Virtual_Available = getData.Data_Virtual_Available;

                // SmallData
                tempData.SmallData_Used = getData.SmallData_Used;
                tempData.SmallData_Available = getData.SmallData_Available;
                tempData.SmallData_Virtual_Used = getData.SmallData_Virtual_Used;
                tempData.SmallData_Virtual_Available = getData.SmallData_Virtual_Available;

                // Load
                tempData.Load_Load = getData.Load_Load;
                tempData.Load_Virtual_Load = getData.Load_Virtual_Load;

                dataList.Add(item.Key, tempData);
            }
            return dataList;
        }
    }
}
