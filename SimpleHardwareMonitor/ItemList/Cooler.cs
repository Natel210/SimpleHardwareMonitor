using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Cooler : AItemList<Data.Cooler, Item.Cooler>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Cooler(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Cooler> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Cooler>();

            foreach (var item in _item)
            {
                var tempData = new Data.Cooler();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
