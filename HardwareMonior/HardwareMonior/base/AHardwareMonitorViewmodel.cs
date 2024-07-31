using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.@base
{
    public abstract class AHardwareMonitorViewmodel : AViewModelBase
    {
        protected AHardwareMonitorViewmodel(SynchronizationContext syncContext) : base(syncContext)
        {

        }

        public bool UpdateData()
        {
            if (_syncContext is null)
                return false;
            try
            {
                bool result = false;
                _syncContext?.Post(_ =>
                {
                    if (HardwareMonitor.Runing is false)
                    {
                        result = false;
                        return;
                    }
                    result = UpdateData_Inner();
                }, null);
                return result;
            }
            catch (Exception) { return false; }
        }

        protected abstract bool UpdateData_Inner();

    }



}
