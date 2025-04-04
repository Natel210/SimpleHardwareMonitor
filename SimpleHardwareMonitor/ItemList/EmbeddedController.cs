using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class EmbeddedController : AItemList<Data.EmbeddedController, Item.EmbeddedController>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.EmbeddedController(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.EmbeddedController> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.EmbeddedController>();

            foreach (var item in _item)
            {
                var tempData = new Data.EmbeddedController();
                var getData = item.Value.Data;



            }

            return dataList;
        }
    }
}
