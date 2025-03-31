using System.Collections.Generic;

namespace SimpleHardwareMonitor.Data
{
    public struct Cpu
    {
        public string Name { get; internal set; }

        public int CoreCount { get; internal set; }

        public int ProcessorCount { get; internal set; }

        #region Use

        /// <summary>
        /// Windows TaskMng's USED Total Datas. <br/>
        /// Unit : %
        /// </summary>
        public float Use { get; internal set; }

        /// <summary>
        /// Windows TaskMng's USED Thread Datas. <br/>
        /// Unit : %
        /// </summary>
        public List<float> Use_ByThreads { get; internal set; }

        #endregion

        #region Load

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Total { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Max { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_ByThreads { get; internal set; }

        #endregion

        #region Temperature

        /// <summary>
        /// Unit : °C
        /// </summary>
        public float Temperature_Package { get; internal set; }

        /// <summary>
        /// Unit : °C
        /// </summary>
        public float Temperature_Max { get; internal set; }

        /// <summary>
        /// Unit : °C
        /// </summary>
        public float Temperature_Average { get; internal set; }

        /// <summary>
        /// Unit : °C
        /// </summary>
        public List<float> Temperature_ByCore { get; internal set; }

        /// <summary>
        /// Unit : °C
        /// </summary>
        public List<float> Temperature_Distanceto_Tj_Max_ByCore { get; internal set; }

        #endregion

        #region Clock

        /// <summary>
        /// Unit : MHz
        /// </summary>
        public float Clock_Bus_Speed { get; internal set; }

        /// <summary>
        /// Unit : MHz
        /// </summary>
        public List<float> Clock_ByCore { get; internal set; }

        #endregion

        #region Voltage

        /// <summary>
        /// Unit : V
        /// </summary>
        public float Voltage { get; internal set; }

        /// <summary>
        /// Unit : V
        /// </summary>
        public List<float> Voltage_ByCore { get; internal set; }

        #endregion

        #region Power

        /// <summary>
        /// Unit : W
        /// </summary>
        public float Power_Package { get; internal set; }

        /// <summary>
        /// Unit : W
        /// </summary>
        public float Power_Cores { get; internal set; }

        /// <summary>
        /// Unit : W
        /// </summary>
        public float Power_Memory { get; internal set; }

        /// <summary>
        /// Unit : W
        /// </summary>
        //public float Power_Graphics { get; internal set; }

        /// <summary>
        /// Unit : W
        /// </summary>
        public float Power_Platform { get; internal set; }

        #endregion
    }
}
