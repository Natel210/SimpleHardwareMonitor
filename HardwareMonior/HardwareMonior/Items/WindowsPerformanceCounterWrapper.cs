using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitor.Items
{
    // 표면적 인터페이스 부분
    public partial class WindowsPerformanceCounterWrapper : IInfoItem, IDisposable
    {
        public void Start()
        {
            lock (_updateLock)
            {
                if (_updateTask != null)
                    return;

                lock (_counterLock)
                {
                    if (_performanceCounter == null)
                    {
                        if (string.IsNullOrEmpty(_categoryName) || string.IsNullOrEmpty(_counterName))
                        {
                            throw new InvalidOperationException("PerformanceCounter properties are not set.");
                        }
                        _performanceCounter = new PerformanceCounter(_categoryName, _counterName, _instanceName);
                    }
                }

                _cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = _cancellationTokenSource.Token;

                _updateTask = Task.Run(async () => await UpdateValueAsync(cancellationToken), cancellationToken);
            }
        }

        public void Stop()
        {
            lock (_updateLock)
            {
                _cancellationTokenSource?.Cancel();
                _updateTask?.Wait();
                _updateTask = null;
                _cancellationTokenSource = null;

                lock (_counterLock)
                {
                    _performanceCounter?.Dispose();
                    _performanceCounter = null;
                }
            }
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public WindowsPerformanceCounterWrapper() { }

        public WindowsPerformanceCounterWrapper(string categoryName, string counterName, string instanceName = "")
        {
            SetProperty(categoryName, counterName, instanceName);
            Start();
        }

        public void SetProperty(string categoryName, string counterName, string instanceName = "", bool autoRestart = false)
        {
            lock (_counterLock)
            {
                _categoryName = categoryName;
                _counterName = counterName;
                _instanceName = instanceName;

                _performanceCounter?.Dispose();
                _performanceCounter = new PerformanceCounter(_categoryName, _counterName, _instanceName);
            }

            if (autoRestart)
                Restart();
        }

        public double GetValue()
        {
            lock (_counterLock)
            {
                return _value;
            }
        }

        public string GetCategoryName()
        {
            lock (_counterLock)
            {
                return _categoryName;
            }
        }

        public string GetCounterName()
        {
            lock (_counterLock)
            {
                return _counterName;
            }
        }

        public string GetInstanceName()
        {
            lock (_counterLock)
            {
                return _instanceName;
            }
        }

        public int GetUpdateInterval()
        {
            lock (_updateLock)
            {
                return _updateInterval;
            }
        }

        public void SetCategoryName(string categoryName, bool autoRestart = false)
        {
            lock (_counterLock)
            {
                _categoryName = categoryName;
                _performanceCounter?.Dispose();
                _performanceCounter = new PerformanceCounter(_categoryName, _counterName, _instanceName);
            }

            if (autoRestart)
                Restart();
        }

        public void SetCounterName(string counterName, bool autoRestart = false)
        {
            lock (_counterLock)
            {
                _counterName = counterName;
                _performanceCounter?.Dispose();
                _performanceCounter = new PerformanceCounter(_categoryName, _counterName, _instanceName);
            }

            if (autoRestart)
                Restart();
        }

        public void SetInstanceName(string instanceName, bool autoRestart = false)
        {
            lock (_counterLock)
            {
                _instanceName = instanceName;
                _performanceCounter?.Dispose();
                _performanceCounter = new PerformanceCounter(_categoryName, _counterName, _instanceName);
            }

            if (autoRestart)
                Restart();
        }

        public void SetUpdateInterval(int interval)
        {
            if (interval <= 0)
            {
                throw new ArgumentException("Interval must be greater than zero.");
            }
            lock (_updateLock)
            {
                _updateInterval = interval;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    // 내부 변수&함수
    public partial class WindowsPerformanceCounterWrapper : IInfoItem, IDisposable
    {
        private readonly object _counterLock = new object();
        private readonly object _updateLock = new object();

        private string _categoryName = null;
        private string _counterName = null;
        private string _instanceName = null;
        private PerformanceCounter _performanceCounter = null;
        private double _value = 0;
        private Task _updateTask = null;
        private CancellationTokenSource _cancellationTokenSource = null;
        private bool _disposed = false;
        private int _updateInterval = 1000; // 기본값은 1000ms (1초)

        ~WindowsPerformanceCounterWrapper()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Stop();
                lock (_counterLock)
                {
                    _performanceCounter?.Dispose();
                }
            }

            _disposed = true;
        }

        private async Task UpdateValueAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                lock (_counterLock)
                {
                    _value = _performanceCounter.NextValue();
                }
                try
                {
                    await Task.Delay(_updateInterval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }
    }
}