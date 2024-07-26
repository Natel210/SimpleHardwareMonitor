using System;
using System.Diagnostics;
using System.Threading;

namespace HardwareMonior.Items
{
    public class WindowsPerformanceCounterWrapper : IDisposable
    {
        public string CategoryName { get; private set; }
        public string CounterName { get; private set; }
        public string InstanceName { get; private set; }
        public double Value { get; private set; }
        private Mutex _settingDataMutex = new Mutex();
        private PerformanceCounter _performanceCounter;
        private bool _disposed = false;

        /// <summary>
        /// 빈 값입니다.<para/>
        /// SetData(...) -> Open()이 필요합니다.<para/>
        /// </summary>
        public WindowsPerformanceCounterWrapper() { }

        public WindowsPerformanceCounterWrapper(string categoryName, string counterName, string instanceName = "")
        {
            SetData(categoryName, counterName, instanceName);
            Open();
        }

        ~WindowsPerformanceCounterWrapper()
        {
            Dispose(false);
        }

        /// <summary>
        /// 퍼포먼스 카운터를 사용하기 위한 데이터를 설정합니다. <para/>
        /// 변경에 대한 적용은 따로 Open 해야합니다.
        /// </summary>
        public bool SetData(string categoryName, string counterName, string instanceName = "")
        {
            // 뮤텍스 접근
            bool accessMutex = _settingDataMutex.WaitOne(0);
            if (accessMutex is false)
                return false;
            bool result = false;
            try
            {
                // 값 넣어줌
                CategoryName = categoryName;
                CounterName = counterName;
                InstanceName = instanceName;
                result = true;
            }
            finally { _settingDataMutex.ReleaseMutex(); }
            return result;
        }

        /// <summary>
        /// 해당 설정에 대해서 탐색을 시작합니다.
        /// </summary>
        public bool Open()
        {
            // 뮤텍스 접근
            bool accessMutex = _settingDataMutex.WaitOne(0);
            if (accessMutex is false)
                return false;
            bool result = false;
            try
            {
                // 생성전에 정리
                if (_performanceCounter != null)
                {
                    _performanceCounter.Close();
                    _performanceCounter.Dispose();
                    _performanceCounter = null;
                }
                // 접근 후 생성
                _performanceCounter = new PerformanceCounter(CategoryName, CounterName, InstanceName);
                result = _performanceCounter != null;
            }
            finally { _settingDataMutex.ReleaseMutex(); }
            return result;
        }

        /// <summary>
        /// 데이터에 대해서 업데이트 되는 구간 입니다.
        /// </summary>
        public double Update()
        {
            if (_performanceCounter is null)
                return 0.0;
            Value = _performanceCounter.NextValue();
            return Value;
        }

        /// <summary>
        /// IDisposable 인터페이스 구현
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 관리 리소스 해제
                _settingDataMutex?.Dispose();
                _performanceCounter?.Dispose();
            }

            // 비관리 리소스 해제 (필요한 경우)
            _disposed = true;
        }
    }
}
