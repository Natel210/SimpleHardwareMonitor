using System.Collections.Generic;

namespace SimpleHardwareMonitor.data
{
    public struct CpuData
    {
        public int CoreCount { get; internal set; }

        public int ProcessorCount { get; internal set; }

        /// <summary>
        /// Windows TaskMng's USED Total Data. </para>
        /// </summary>
        public float Use { get; internal set; }

        /// <summary>
        /// Windows TaskMng's USED Thread Datas. </para>
        /// </summary>
        public List<float> UseByThreads { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Voltage { get; internal set; }
        /// <summary>
        /// voltage details per core. </para>
        /// </summary>
        public List<float> VoltageByCore { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Power { get; internal set; }
        /// <summary>
        /// power details per core. </para>
        /// </summary>
        public List<float> PowerByCore { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Temperature { get; internal set; }
        /// <summary>
        /// temperature details per core. </para>
        /// </summary>
        public List<float> TemperatureByCore { get; internal set; }
    }
}
