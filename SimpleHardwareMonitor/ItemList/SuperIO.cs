using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class SuperIO : AItemList<Data.SuperIO, Item.SuperIO>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.SuperIO(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.SuperIO> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.SuperIO>();

            foreach (var item in _item)
            {
                var tempData = new Data.SuperIO();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
