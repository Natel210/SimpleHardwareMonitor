using System;
using System.Threading;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.@base
{
    public enum UpdateIntervalMode
    {
        Integrated,
        Independent
    }

    // interface of surface.
    public abstract partial class AHardwareMonitor<T> : IDisposable where T : struct
    {
        /// <summary>
        /// get data.
        /// </summary>
        public T Data { get { return _data; } }
        public AHardwareMonitor(IHardware hardware)
        {
            _hardware = hardware ?? throw new ArgumentNullException(nameof(hardware), "Hardware cannot be null");
            _cancellationTokenSource = new CancellationTokenSource();
            Init();
            var cancellationToken = _cancellationTokenSource.Token;
            _updateTask = Task.Run(async () => await UpdateTaskAsync(cancellationToken), cancellationToken);
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
    }

    // inheritance methods.
    public abstract partial class AHardwareMonitor<T>
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
    public abstract partial class AHardwareMonitor<T>
    {
        /// <summary>
        /// task instance.
        /// </summary>
        private Task _updateTask = null;
        /// <summary>
        /// to cancel an asynchronous operation.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = null;

        /// <summary>
        /// to synchronize state changes.
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// update asynchronous behavior inside.
        /// </summary>
        private async Task UpdateTaskAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _hardware.Update();
                    Update();
                    await Task.Delay(50);
                }
            }
            catch (TaskCanceledException)
            {
                // task was canceled, no need to handle this as an error.
            }
            catch (Exception ex)
            {
                // rethrow the exception to be handled by the caller.
                throw new Exception($"Exception occurred during update: {ex.Message}", ex);
            }
        }
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
                            _cancellationTokenSource?.Cancel();
                            try
                            {
                                _updateTask?.Wait();
                            }
                            catch (AggregateException/* ex*/)
                            {
                                //// Handle any exceptions thrown by the task
                                //foreach (var innerEx in ex.InnerExceptions)
                                //{
                                //    throw new Exception($"Exception in task: {innerEx.Message}", innerEx);
                                //}
                            }
                            _cancellationTokenSource?.Dispose();

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