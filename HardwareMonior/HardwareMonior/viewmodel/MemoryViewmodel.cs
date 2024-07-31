using SimpleHardwareMonitor.@base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class MemoryViewmodel : AHardwareMonitorViewmodel
    {
        private float _use;
        public float Use
        {
            get => _use;
            private set => Set(ref _use, value);
        }
    }

    public partial class MemoryViewmodel : AHardwareMonitorViewmodel
    {
        public MemoryViewmodel(SynchronizationContext syncContext) : base(syncContext) { }
        protected override bool UpdateData_Inner()
        {
            Use = 3;
            return true;
        }
    }
}
