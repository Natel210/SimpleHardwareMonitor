using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents cooler-related sensor data model.
    /// </summary>
    public struct Cooler
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name of the cooler device.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for voltage-related metrics for the cooler (e.g., fan motor voltage)
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current metrics related to cooler operation
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for power consumption data of the cooler
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Not typically applicable to cooler devices
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for cooler-related temperature data (e.g., heat sink sensors)
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Reserved for load or efficiency metrics of the cooler
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for operational frequency (if any) for advanced coolers
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Reserved for fan speed metrics (e.g., RPM)
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for liquid cooler flow rate data (e.g., L/h)
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for control signals or PWM values for fan control
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for levels or thresholds (e.g., fluid level in liquid cooling)
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for any general large-scale data values
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for compact data representation related to the cooler
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for cooler throughput (e.g., air or liquid flow throughput)
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for operational time or uptime metrics
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for energy consumption metrics of the cooler
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for acoustic performance data (e.g., dBA noise levels)
        #endregion

    }
}