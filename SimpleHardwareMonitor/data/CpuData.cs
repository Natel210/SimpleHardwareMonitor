
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        /// package
        /// </summary>
        public float Power { get; internal set; }
        /// <summary>
        /// package
        /// </summary>
        public float Temperature { get; internal set; }
    }
}
