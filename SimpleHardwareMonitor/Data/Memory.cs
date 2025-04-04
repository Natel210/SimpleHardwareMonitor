namespace SimpleHardwareMonitor.Data
{
    public struct Memory
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Memory { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Virtual_Memory { get; internal set; }

        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
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

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data

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

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        #endregion

    }
}
