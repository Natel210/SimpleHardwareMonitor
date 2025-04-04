using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Network : AItemList<Data.Network, Item.Network>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Network(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Network> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Network>();

            foreach (var item in _item)
            {
                var tempData = new Data.Network();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
