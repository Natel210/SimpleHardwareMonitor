using System;

namespace SimpleHardwareMonitorGUI.Common.SyncThread
{
    /// <summary>
    /// A thread-safe wrapper for string values.
    /// Ensures safe concurrent access to the underlying string using a locking mechanism.
    /// </summary>
    internal class SyncString : ISync<string>
    {
        private string _value;
        private readonly object _mutex = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncString"/> class with the specified value.
        /// </summary>
        /// <param name="value">The initial string value.</param>
        public SyncString(string value)
        {
            lock (_mutex)
                _value = value;
        }

        /// <inheritdoc/>
        public string Value
        {
            get
            {
                lock (_mutex)
                    return _value;
            }
            set
            {
                lock (_mutex)
                {
                    if (_value != value)
                        _value = value;
                }
            }
        }

        public static implicit operator string(SyncString syncString) => syncString.Value;
        public static implicit operator SyncString(string value) => new(value);
        public override string ToString() => _value;
    }
}
