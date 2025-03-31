using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.ItemList
{
    internal abstract class AItemList<TData, TItem> where TData : struct where TItem : Item.AItem<TData>
    {
        internal Dictionary<string, TData> Data { get => MakeDataList(); }

        protected Dictionary<string, TItem> _item = new Dictionary<string, TItem>();
        private Mutex _itemMutex = new Mutex();

        internal void Add(IHardware hardware)
        {
            lock (_itemMutex)
            {
                AddChild(hardware);
            }
        }

        protected abstract void AddChild(IHardware hardware);

        internal void Clear()
        {
            lock (_itemMutex)
                _item.Clear();
        }

        internal bool Update(IHardware hardware)
        {
            lock (_itemMutex)
            {
                if (_item.TryGetValue(hardware.Name, out var item))
                    return item.Update(hardware);
            }
            return false;
        }

        private Dictionary<string, TData> MakeDataList()
        {
            return MakeDataListChild();
        }

        protected abstract Dictionary<string, TData> MakeDataListChild();
    }
}
