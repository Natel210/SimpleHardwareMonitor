using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.common;
using SimpleHardwareMonitor.data;
using SimpleHardwareMonitor.viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class StorageMonitor : AHardwareMonitor<StorageData>
    {
        public StorageMonitor(IHardware hardware) : base(hardware) { }

        protected sealed override void Init()
        {
            ManagementObjectSearcher searcherNameItem = new ManagementObjectSearcher("select Model, Name, Size from Win32_DiskDrive");
            _data.storageDataNameItems = new List<StorageDataNameItem>();
            foreach (ManagementObject obj in searcherNameItem.Get())
            {
                var item = new StorageDataNameItem();

                item.Name = obj["Name"]?.ToString() ?? "Unknown Name";
                item.Model = obj["Model"]?.ToString() ?? "Unknown Model";
                item.Size = obj["Size"] != null ? Convert.ToInt64(obj["Size"]) : 0;

                _data.storageDataNameItems.Add(item);
            }
            StorageVM.instance.StorageDataNameItems = listToObservableCollection(_data.storageDataNameItems);
        }

        protected sealed override void PrevUpdate()
        {

        }

        private ObservableCollection<StorageDataNameItem> listToObservableCollection(List<StorageDataNameItem> src)
        {
            ObservableCollection<StorageDataNameItem> temp = new ObservableCollection<StorageDataNameItem>();
            foreach (var item in src)
                temp.Add(item);
            return temp;
        }
    }
}
