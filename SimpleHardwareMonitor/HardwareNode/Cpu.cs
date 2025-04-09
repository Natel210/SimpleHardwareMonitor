using System.Collections.Generic;
using System.Text.RegularExpressions;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal partial class Cpu : AHardwareNode<Model.Cpu>
    {
        internal Cpu(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        protected sealed override void Init()
        {
            _model.Load_ByThreads = new Dictionary<int, Dictionary<int, float>>();
            _model.Temperature_ByCore = new Dictionary<int, float>();
            _model.Temperature_Distanceto_Tj_Max_ByCore = new Dictionary<int, float>();
            _model.Clock_ByCore = new Dictionary<int, float>();
            _model.Voltage_ByCore = new Dictionary<int, float>();
        }

        protected sealed override bool PrevUpdate()
        {
            _model.Load_ByThreads.Clear();
            _model.Temperature_ByCore.Clear();
            _model.Temperature_Distanceto_Tj_Max_ByCore.Clear();
            _model.Clock_ByCore.Clear();
            _model.Voltage_ByCore.Clear();

            return true;
        }

        protected sealed override bool CustomUpdateToSensor(ISensor sensor)
        {
            var name = sensor.Name.TrimEnd('\0').ToLower();

            if (sensor.SensorType == SensorType.Voltage)
            {
                var regex = new Regex(@"^cpu core #(?<core>\d+)$", RegexOptions.IgnoreCase);
                var match = regex.Match(name);

                if (match.Success && int.TryParse(match.Groups["core"].Value, out int coreIndex))
                {
                    _model.Voltage_ByCore[coreIndex] = sensor.Value ?? -1;
                    return true;
                }
            }
            else if (sensor.SensorType == SensorType.Clock)
            {
                var regex = new Regex(@"^cpu core #(?<core>\d+)$", RegexOptions.IgnoreCase);
                var match = regex.Match(name);

                if (match.Success && int.TryParse(match.Groups["core"].Value, out int coreIndex))
                {
                    _model.Clock_ByCore[coreIndex] = sensor.Value ?? -1;
                    return true;
                }
            }
            else if (sensor.SensorType == SensorType.Temperature)
            {
                var regexCore = new Regex(@"^cpu core #(?<core>\d+)$", RegexOptions.IgnoreCase);
                var matchCore = regexCore.Match(name);

                if (matchCore.Success && int.TryParse(matchCore.Groups["core"].Value, out int coreIndex))
                {
                    _model.Temperature_ByCore[coreIndex] = sensor.Value ?? -1;
                    return true;
                }

                var regexTj = new Regex(@"^cpu core #(?<core>\d+) distance to tjmax$", RegexOptions.IgnoreCase);
                var matchTj = regexTj.Match(name);

                if (matchTj.Success && int.TryParse(matchTj.Groups["core"].Value, out int tjIndex))
                {
                    _model.Temperature_Distanceto_Tj_Max_ByCore[tjIndex] = sensor.Value ?? -1;
                    return true;
                }
            }
            else if (sensor.SensorType == SensorType.Load)
            {
                var regex = new Regex(@"^cpu core #(?<core>\d+)\s+thread #(?<thread>\d+)$", RegexOptions.IgnoreCase);
                var match = regex.Match(name);

                if (match.Success &&
                    int.TryParse(match.Groups["core"].Value, out int coreIndex) &&
                    int.TryParse(match.Groups["thread"].Value, out int threadIndex))
                {
                    if (!_model.Load_ByThreads.ContainsKey(coreIndex))
                        _model.Load_ByThreads[coreIndex] = new Dictionary<int, float>();

                    _model.Load_ByThreads[coreIndex][threadIndex] = sensor.Value ?? -1;
                    return true;
                }
            }
            return false;
        }

        protected sealed override void RegisterVoltageSensorMethods()
        {
            _updateSensorMethods[SensorType.Voltage] = new Functional.SensorMethodItem() {
                { "cpu core", (ISensor sensor) => { _model.Voltage = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterPowerSensorMethods()
        {
            _updateSensorMethods[SensorType.Power] = new Functional.SensorMethodItem() {
                { "cpu package", (ISensor sensor) => { _model.Power_Package = sensor.Value ?? -1; } },
                { "cpu cores", (ISensor sensor) => { _model.Power_Cores = sensor.Value ?? -1; } },
                { "cpu memory", (ISensor sensor) => { _model.Power_Memory = sensor.Value ?? -1; } },
                { "cpu platform", (ISensor sensor) => { _model.Power_Platform = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterClockSensorMethods()
        {
            _updateSensorMethods[SensorType.Voltage] = new Functional.SensorMethodItem() {
                { "bus speed", (ISensor sensor) => { _model.Voltage = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterTemperatureSensorMethods()
        {
            _updateSensorMethods[SensorType.Temperature] = new Functional.SensorMethodItem() {
                { "cpu package", (ISensor sensor) => { _model.Temperature_Package = sensor.Value ?? -1; } },
                { "core max", (ISensor sensor) => { _model.Temperature_Max = sensor.Value ?? -1; } },
                { "core average", (ISensor sensor) => { _model.Temperature_Average = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterLoadSensorMethods()
        {
            _updateSensorMethods[SensorType.Load] = new Functional.SensorMethodItem() {
                { "cpu total", (ISensor sensor) => { _model.Load_Total = sensor.Value ?? -1; } },
                { "cpu core max", (ISensor sensor) => { _model.Load_Core_Max = sensor.Value ?? -1; } },
            };
        }
    }
}
