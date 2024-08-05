using System;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.@base
{
    // interface of surface.
    internal abstract partial class AHardwareMonitor<T> : IDisposable where T : struct
    {
        /// <summary>
        /// get data.
        /// </summary>
        public T Data { get { return _data; } }
        internal AHardwareMonitor(IHardware hardware)
        {
            _hardware = hardware ?? throw new ArgumentNullException(nameof(hardware), "Hardware cannot be null");
            Init();
        }
        /// <summary>
        /// to track whether dispose has been called.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;
        /// <summary>
        /// implementing idisposable pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose_Inner(true);
            GC.SuppressFinalize(this);
        }
        internal void UpdateHardWare()
        {
            _hardware.Update();
            Update();
        }
    }

    // inheritance methods.
    internal abstract partial class AHardwareMonitor<T>
    {
        /// <summary>
        /// data instance.
        /// </summary>
        protected T _data = new T();
        /// <summary>
        /// librehardwaremonitor instance.
        /// </summary>
        protected readonly IHardware _hardware;
        /// <summary>
        /// additional initialization required during creation.
        /// </summary>
        protected virtual void Init() { }
        /// <summary>
        /// update logic to insert values into the actual [T] structure.
        /// </summary>
        protected abstract void Update();
        /// <summary>
        /// method that can be overridden in derived classes to clean up resources.
        /// </summary>
        protected virtual void Dispose(bool disposing) { }
    }

    // backend components.
    internal abstract partial class AHardwareMonitor<T>
    {
        /// <summary>
        /// to synchronize state changes.
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// dispose internal function.
        /// </summary>
        private void Dispose_Inner(bool disposing)
        {
            if (!IsDisposed)
            {
                lock (_lock)
                {
                    if (!IsDisposed)
                    {
                        if (disposing)
                        {
                            // call the dispose method of the derived class.
                            Dispose(disposing); 
                            // dispose _hardware if it implements idisposable.
                            if (_hardware is IDisposable disposableHardware)
                            {
                                disposableHardware.Dispose();
                            }
                        }
                        IsDisposed = true;
                    }
                }
            }
        }
        ~AHardwareMonitor()
        {
            Dispose_Inner(false);
        }
    }
}