using System;
using System.Collections.Generic;
using System.Text;

namespace HardwareMonitor.Items
{
    public interface IInfoItem
    {
        void Start();
        void Stop();
        void Restart();

        int GetUpdateInterval();
        void SetUpdateInterval(int interval);

        double GetValue();
    }
}
