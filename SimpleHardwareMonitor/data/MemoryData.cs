namespace SimpleHardwareMonitor.data
{
    public struct MemoryData
    {
        public float Use { get; internal set; }
        public float Value { get; internal set; }
        public float Available { get; internal set; }
        
        public float VirtualUse { get; internal set; }
        public float VirtualValue { get; internal set; }
        public float VirtualAvailable { get; internal set; }
    }
}
