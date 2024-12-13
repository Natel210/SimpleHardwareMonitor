using System.Collections.Generic;

namespace SimpleHardwareMonitor.data
{
    public struct StorageDataNameItem
    {
        public string Name;
        public string Model;
        public long Size;
    }


    public struct StorageData
    {
        public List<StorageDataNameItem> storageDataNameItems { get; internal set; }

    }
}
