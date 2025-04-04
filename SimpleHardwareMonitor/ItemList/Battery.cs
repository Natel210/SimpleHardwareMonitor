using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Battery : AItemList<Data.Battery, Item.Battery>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Battery(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Battery> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Battery>();

            foreach (var item in _item)
            {
                var tempData = new Data.Battery();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
