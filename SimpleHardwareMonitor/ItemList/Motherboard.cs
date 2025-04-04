using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Motherboard : AItemList<Data.Motherboard, Item.Motherboard>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Motherboard(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Motherboard> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Motherboard>();

            foreach (var item in _item)
            {
                var tempData = new Data.Motherboard();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
