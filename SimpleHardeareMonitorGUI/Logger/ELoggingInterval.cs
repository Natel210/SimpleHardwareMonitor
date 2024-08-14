using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardwareMonitorGUI.Logger
{
    public enum ELoggingInterval
    {
        ms250 = 250,
        ms500 = 500,
        s1 = 1000,
        s5 = 5000,
        s10 = 10000,
        s20 = 20000,
        s30 = 30000,
        m1 = 60000,
        m10 = 600000,
        m20 = 1200000,
        m30 = 1800000,
        h1 = 3600000,
    }
}
