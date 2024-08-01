using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardwareMonitorGUI
{
    public enum EUpdateIntervalTime
    {
        ms100,
        s1,
        s5,
        m1,
    }


    static class SettingData
    {
        public static EUpdateIntervalTime UpdateIntervalTime { get; set; } = EUpdateIntervalTime.ms100;
        public static string CurrentUpdateIntervalTimeToString()
        {
            string temp = "";
            switch (UpdateIntervalTime)
            {
                case EUpdateIntervalTime.ms100:
                    temp = "100ms";
                    break;
                case EUpdateIntervalTime.s1:
                    temp = "1s";
                    break;
                case EUpdateIntervalTime.s5:
                    temp = "5s";
                    break;
                case EUpdateIntervalTime.m1:
                    temp = "1m";
                    break;
                default:
                    break;
            }
            return temp;
        }

        public static int CurrentUpdateIntervalTimeToInteger()
        {
            int temp = -1;
            switch (UpdateIntervalTime)
            {
                case EUpdateIntervalTime.ms100:
                    temp = 100;
                    break;
                case EUpdateIntervalTime.s1:
                    temp = 1000;
                    break;
                case EUpdateIntervalTime.s5:
                    temp = 5000;
                    break;
                case EUpdateIntervalTime.m1:
                    temp = 60000;
                    break;
                default:
                    break;
            }
            return temp;
        }


    }
}
