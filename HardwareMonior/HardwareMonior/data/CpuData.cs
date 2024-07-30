
namespace SimpleHardwareMonitor.data
{
    public struct CpuData
    {
        /// <summary>
        /// Windows TaskMng's USED Data
        /// </summary>
        public float Use { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Voltage { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Power { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Temperature { get; internal set; }
    }
}
