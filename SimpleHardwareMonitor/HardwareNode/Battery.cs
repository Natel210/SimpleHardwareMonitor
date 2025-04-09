using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal partial class Battery : AHardwareNode<Model.Battery>
    {
        internal Battery(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        protected sealed override void RegisterVoltageSensorMethods()
        {
            _updateSensorMethods[SensorType.Voltage] = new Functional.SensorMethodItem() {
                { "voltage", (ISensor sensor) => { _model.Voltage = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterCurrentSensorMethods()
        {
            _updateSensorMethods[SensorType.Current] = new Functional.SensorMethodItem() {
                { "charge/discharge current", (ISensor sensor) => { _model.Current_Charge_Discharge = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterPowerSensorMethods()
        {
            _updateSensorMethods[SensorType.Power] = new Functional.SensorMethodItem() {
                { "charge/discharge rate", (ISensor sensor) => { _model.Power_Charge_Discharge_Rate = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterLevelSensorMethods()
        {
            _updateSensorMethods[SensorType.Level] = new Functional.SensorMethodItem() {
                { "degradation level", (ISensor sensor) => { _model.Level_Degradation = sensor.Value ?? -1; } },
                  { "charge level", (ISensor sensor) => { _model.Level_Charge = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterEnergySensorMethods()
        {
            _updateSensorMethods[SensorType.Energy] = new Functional.SensorMethodItem() {
                { "designed capacity", (ISensor sensor) => { _model.Energy_Designed_Capacity = sensor.Value ?? -1; } },
                { "fully-charged capacity", (ISensor sensor) => { _model.Energy_Fully_Charged_Capacity = sensor.Value ?? -1; } },
                { "remaining capacity", (ISensor sensor) => { _model.Energy_Remaining_Capacity = sensor.Value ?? -1; } },
            };
        }

    }
}
