using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents network adapter-related sensor data model.<br/>
    /// Includes common identification and placeholder regions for throughput, energy, and interface-level metrics.
    /// </summary>
    public struct Network
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or identifier of the network adapter.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for voltage measurements for network controller circuitry
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current draw metrics of the network interface
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for power usage of wired or wireless adapter
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Reserved for internal controller clock or PHY clock values
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for thermal sensor on network chipsets (if available)
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Reserved for utilization/load metrics of network interface
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for operational frequency of wireless interfaces (e.g., 2.4GHz, 5GHz)
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Typically not applicable to network components
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for packet flow metrics or buffer-level monitoring
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for control signals such as link enable, power-saving state
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for signal strength or quality levels
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for transferred data volumes (e.g., TX/RX bytes)
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for smaller metrics such as short-term packet rates
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for bandwidth usage, transmission rate, etc.
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for connection duration or activity period tracking
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for power consumption over time (e.g., mWh for Wi-Fi)
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for RF noise level or link interference indicators
        #endregion
    }
}