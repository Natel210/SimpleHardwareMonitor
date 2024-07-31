using SimpleHardwareMonitor;
using System;
using System.Threading;

namespace SimpleHardwareMonitorGUI
{
    internal partial class MonitorInterface : AViewModelBase
    {
        private readonly SynchronizationContext _syncContext;
        private readonly Timer _hardwareMonitorTimer;

#pragma warning disable CS8618 // 생성자를 종료할 때 null을 허용하지 않는 필드에 null이 아닌 값을 포함해야 합니다. null 허용으로 선언해 보세요.
        internal MonitorInterface(SynchronizationContext syncContext)
#pragma warning restore CS8618 // 생성자를 종료할 때 null을 허용하지 않는 필드에 null이 아닌 값을 포함해야 합니다. null 허용으로 선언해 보세요.
        {
#pragma warning disable CS8601 // 가능한 null 참조 할당입니다.
            _syncContext = syncContext ?? SynchronizationContext.Current;
#pragma warning restore CS8601 // 가능한 null 참조 할당입니다.
            _hardwareMonitorTimer = new Timer(HardwareMonitorTimer, null, 1000, 100);
        }

        private void HardwareMonitorTimer(object? state)
        {
            try
            {
                _syncContext?.Post(_ =>
                {
                    if (HardwareMonitor.Runing is false)
                        return;
                    CpuUse = HardwareMonitor.Cpu.Data.Use;
                    CpuVoltage = HardwareMonitor.Cpu.Data.Voltage;
                    CpuPower = HardwareMonitor.Cpu.Data.Power;
                    CpuTemperature = HardwareMonitor.Cpu.Data.Temperature;
                }, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Timer callback exception: {ex.Message}");
            }
        }


    }





    internal partial class MonitorInterface : AViewModelBase
    {
        private float _cpuUse;
        public float CpuUse
        {
            get => _cpuUse;
            set => Set(ref _cpuUse, value);
        }

        private float _cpuVoltage;
        public float CpuVoltage
        {
            get => _cpuVoltage;
            set => Set(ref _cpuVoltage, value);
        }

        private float _cpuPower;
        public float CpuPower
        {
            get => _cpuPower;
            set => Set(ref _cpuPower, value);
        }

        private float _cpuTemperature;
        public float CpuTemperature
        {
            get => _cpuTemperature;
            set => Set(ref _cpuTemperature, value);
        }
    }


}