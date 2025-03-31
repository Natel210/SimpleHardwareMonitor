namespace SimpleHardwareMonitor.Data
{
    public struct Memory
    {
        public string Name { get; internal set; }

        #region Data

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Used"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Used { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Available"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Available { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Virtual_Used"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Virtual_Used { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Virtual_Available"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Virtual_Available { get; internal set; }

        #endregion

        #region SmallData

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Used"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Used { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Available"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Available { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Virtual_Used"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Virtual_Used { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Virtual_Available"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Virtual_Available { get; internal set; }

        #endregion

        #region Load

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Load { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Virtual_Load { get; internal set; }

        #endregion



    }
}
