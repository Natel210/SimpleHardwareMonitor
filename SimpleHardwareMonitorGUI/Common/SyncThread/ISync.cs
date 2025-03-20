using System;

namespace SimpleHardwareMonitorGUI.Common.SyncThread
{
    /// <summary>
    /// Defines a thread-safe wrapper interface for storing values.
    /// </summary>
    /// <typeparam name="T">The type of value being synchronized.</typeparam>
    internal interface ISync<T>
    {
        /// <summary>
        /// Gets or sets the value in a thread-safe manner.
        /// </summary>
        T Value { get; set; }
    }
}
