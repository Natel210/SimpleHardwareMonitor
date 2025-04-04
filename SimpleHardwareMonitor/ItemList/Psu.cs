using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Psu : AItemList<Data.Psu, Item.Psu>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Psu(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Psu> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Psu>();

            foreach (var item in _item)
            {
                var tempData = new Data.Psu();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
