using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents Super I/O controller-related sensor data model.<br/>
    /// Often used for fan, temperature, and voltage monitoring.
    /// </summary>
    public struct SuperIO
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or identifier of the Super I/O controller.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for voltage sensors (e.g., Vcore, VCC, 3.3V)
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current sensors (if supported by controller)
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for power usage or delivery data
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Reserved for monitoring internal clock or bus clocks
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for onboard temperature sensors (e.g., CPU, system)
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Reserved for fan load or voltage regulation loads
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for fan tachometer frequency or PWM cycle rates
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Reserved for fan speed values (e.g., SYS_FAN, CPU_FAN)
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for fluid flow sensors (if applicable)
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for fan control values (PWM duty cycle)
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for signal or thermal level data
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for general telemetry or sensor dumps
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for compact telemetry or byte-based summaries
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for fan speed or system throughput monitoring (if available)
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for total operational time of sensors/fans
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for tracking energy usage (if supported)
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for audible or electrical noise measurement
        #endregion
    }
}