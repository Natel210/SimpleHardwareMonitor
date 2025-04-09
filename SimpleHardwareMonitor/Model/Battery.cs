using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents battery-related sensor data model.
    /// </summary>
    public struct Battery
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name of the battery device.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage

        /// <summary>
        /// Current battery voltage.<br/>
        /// Unit: V (Volts)
        /// </summary>
        public float Voltage { get; internal set; }

        #endregion

        /*---- [ Current ] ----------------------------------------------------*/
        #region Current

        /// <summary>
        /// Charging or discharging current.<br/>
        /// Unit: A (Amperes)
        /// </summary>
        public float Current_Charge_Discharge { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for future voltage-related metrics
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for additional current-related metrics
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power

        /// <summary>
        /// Rate of power being charged or discharged.<br/>
        /// Unit: W (Watts)
        /// </summary>
        public float Power_Charge_Discharge_Rate { get; internal set; }

        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Not applicable for battery
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for battery temperature metrics
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Not applicable for battery
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Not applicable for battery
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Not applicable for battery
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Not applicable for battery
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for battery control information
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level

        /// <summary>
        /// Indicates battery wear level or degradation.<br/>
        /// Unit: %
        /// </summary>
        public float Level_Degradation { get; internal set; }

        /// <summary>
        /// Current battery charge level.<br/>
        /// Unit: %
        /// </summary>
        public float Level_Charge { get; internal set; }

        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for large-scale battery data metrics
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for compact data values
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for data transfer or energy flow rate
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for duration-based battery stats
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy

        /// <summary>
        /// Original design capacity of the battery.<br/>
        /// Unit: mWh (milliwatt-hours)
        /// </summary>
        public float Energy_Designed_Capacity { get; internal set; }

        /// <summary>
        /// Maximum capacity when fully charged.<br/>
        /// Unit: mWh (milliwatt-hours)
        /// </summary>
        public float Energy_Fully_Charged_Capacity { get; internal set; }

        /// <summary>
        /// Current remaining energy in the battery.<br/>
        /// Unit: mWh (milliwatt-hours)
        /// </summary>
        public float Energy_Remaining_Capacity { get; internal set; }

        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Not applicable for battery
        #endregion

    }
}
