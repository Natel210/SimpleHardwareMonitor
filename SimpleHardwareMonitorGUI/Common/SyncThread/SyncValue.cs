using System;
using System.Collections.Generic;

namespace SimpleHardwareMonitorGUI.Common.SyncThread
{
    /// <summary>
    /// A thread-safe wrapper for value types (e.g., int, short, char, etc.).
    /// This class ensures safe concurrent access to the underlying value using a locking mechanism.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    internal class SyncValue<T> : ISync<T> where T : struct
    {
        private T _value;
        private readonly object _mutex = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncValue{T}"/> class with the specified value.
        /// </summary>
        /// <param name="value">The initial value to store.</param>
        public SyncValue(T value)
        {
            _value = value;
        }

        /// <inheritdoc/>
        public T Value
        {
            get
            {
                lock (_mutex)
                {
                    return _value;
                }
            }
            set
            {
                lock (_mutex)
                {
                    if (!EqualityComparer<T>.Default.Equals(_value, value))
                        _value = value;
                }
            }
        }

        public static implicit operator T(SyncValue<T> syncValue) => syncValue.Value;
        public static implicit operator SyncValue<T>(T value) => new(value);
        public override string? ToString() => Value.ToString();
    }
}
