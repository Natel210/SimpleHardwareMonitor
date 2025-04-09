using System;
using System.Diagnostics;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    /// <summary>
    /// The AItem class is an abstract class designed to manage and update hardware sensor data.<br/>
    /// This class is designed to handle data from various hardware sensors.<br/>
    /// It provides methods for updating hardware sensor data and managing resources.<br/>
    /// </summary>
    /// <typeparam name="TModel">The structure type for the hardware data model.</typeparam>
    internal abstract partial class AHardwareNode<TModel> where TModel : struct
    {
        /// <summary>
        /// data instance.
        /// </summary>
        protected TModel _model = new TModel();

        internal TModel Model { get => _model; }
        internal string HardWareName { get; private set; }
        internal HardwareType HardwareType { get; private set; }
        internal AHardwareNode(string hardWareName, HardwareType hardwareType)
        {
            HardWareName = hardWareName;
            HardwareType = hardwareType;
            Init();
            RegisterSensorMethods();
        }

        /// <summary>
        /// Registers sensor handling methods for each sensor type (e.g., Voltage, Power, Temperature, etc.).
        /// These methods are stored and used later to process real-time sensor data.
        /// </summary>
        private void RegisterSensorMethods()
        {
            _updateSensorMethods.Clear();
            RegisterVoltageSensorMethods();
            RegisterCurrentSensorMethods();
            RegisterPowerSensorMethods();
            RegisterClockSensorMethods();
            RegisterTemperatureSensorMethods();
            RegisterLoadSensorMethods();
            RegisterFrequencySensorMethods();
            RegisterFanSensorMethods();
            RegisterFlowSensorMethods();
            RegisterControlSensorMethods();
            RegisterLevelSensorMethods();
            RegisterDataSensorMethods();
            RegisterSmallDataSensorMethods();
            RegisterThroughputSensorMethods();
            RegisterTimeSpanSensorMethods();
            RegisterEnergySensorMethods();
            RegisterNoiseSensorMethods();
        }

        /// <summary>
        /// Updates hardware-related data for each sensor on the system.<br/>
        /// This method is composed of the following steps:<br/>
        /// <list type="bullet">
        ///   <item><description>Initial updates via <see cref="PrevUpdate()"/>.</description></item>
        ///   <item><description>Sensor-specific updates handled by <see cref="UpdateToSensor(ISensor)"/> for each hardware sensor type.</description></item>
        ///   <item><description>Final updates via <see cref="PostUpdate()"/>.</description></item>
        /// </list>
        /// <para>
        /// The sensor-specific updates are delegated to the <see cref="UpdateToSensor(ISensor)"/> method, 
        /// which routes the update to the appropriate method based on the <see cref="SensorType"/>.
        /// Please refer to the protected virtual methods for each sensor type update logic.
        /// </para>
        /// </summary>
        /// <returns>Returns <c>true</c> if all updates are successful, otherwise <c>false</c>.</returns>
        internal bool Update(IHardware hardware)
        {
            if (hardware is null)
                return false;
            bool reverseResult = false;

            hardware.Update();
            foreach (var subHardware in hardware.SubHardware)
                subHardware.Update();
            reverseResult |= !PrevUpdate();
            foreach (var sensor in hardware.Sensors)
            {
                bool isSensorUpdate = UpdateToSensor(sensor);
                if (isSensorUpdate == false)
                {
                    isSensorUpdate = CustomUpdateToSensor(sensor);
                    if (isSensorUpdate == false)
                        Debug.WriteLine($"[Error][Invaild][Hardware]{hardware.HardwareType} : {hardware.Name.TrimEnd('\0')}    [Sensor]{sensor.SensorType} : {sensor.Name.TrimEnd('\0')}");
                }
                reverseResult |= !isSensorUpdate;
            }

            reverseResult |= !PostUpdate();
            return !reverseResult;
        }
    }

    //Update Logic
    internal abstract partial class AHardwareNode<TModel> where TModel : struct
    {
        protected Functional.SensorMethod _updateSensorMethods = new Functional.SensorMethod();

        /// <summary>
        /// Additional initialization required during creation.
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// Prepares and updates the <see cref="AHardwareNode{TData}"/> structure by inserting values into its data.
        /// This method defines what values need to be updated in advance based on the <typeparamref name="TModel"/> type.
        /// Derived classes should override this method to implement custom update logic specific to <typeparamref name="TModel"/>.
        /// </summary>
        protected virtual bool PrevUpdate() { return true; }

        /// <summary>
        /// Performs a custom update to the sensor if special logic is required.
        /// Override this method to implement sensor-specific custom behavior.
        /// </summary>
        /// <param name="sensor">The sensor instance to apply custom logic to.</param>
        /// <returns>True if the update was handled by custom logic; otherwise, false.</returns>
        protected virtual bool CustomUpdateToSensor(ISensor sensor) { return false; }

        /// <summary>
        /// Updates the sensor data based on the sensor type.
        /// This method delegates the update logic to the corresponding update method for each sensor type.
        /// </summary>
        /// <param name="sensor">The sensor whose data needs to be updated.</param>
        /// <returns>Returns <c>true</c> if the update is successful, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">Thrown when an unsupported sensor type is encountered.</exception>
        private bool UpdateToSensor(ISensor sensor)
        {
            if (sensor is null)
                return false;
            if (_updateSensorMethods.ContainsKey(sensor.SensorType) is false)
                return false;
            var updateMethodItem = _updateSensorMethods[sensor.SensorType];
            if (updateMethodItem.ContainsKey(sensor.Name.TrimEnd('\0').ToLower()) is false)
                return false;
            updateMethodItem[sensor.Name.ToLower()](sensor);
            return true;
        }

        /// <summary>
        /// Final update step that performs any remaining updates after sensor-specific updates.
        /// Derived classes can override this method to implement any custom logic that should run after the sensor updates.
        /// </summary>
        /// <returns>Returns <c>true</c> if the final update step is successful, otherwise <c>false</c>.</returns>
        protected virtual bool PostUpdate() { return true; }


        protected virtual void RegisterVoltageSensorMethods() {}
        protected virtual void RegisterCurrentSensorMethods() {}
        protected virtual void RegisterPowerSensorMethods() {}
        protected virtual void RegisterClockSensorMethods() {}
        protected virtual void RegisterTemperatureSensorMethods() {}
        protected virtual void RegisterLoadSensorMethods() {}
        protected virtual void RegisterFrequencySensorMethods() {}
        protected virtual void RegisterFanSensorMethods() {}
        protected virtual void RegisterFlowSensorMethods() {}
        protected virtual void RegisterControlSensorMethods() {}
        protected virtual void RegisterLevelSensorMethods() {}
        protected virtual void RegisterDataSensorMethods() {}
        protected virtual void RegisterSmallDataSensorMethods() {}
        protected virtual void RegisterThroughputSensorMethods() {}
        protected virtual void RegisterTimeSpanSensorMethods() {}
        protected virtual void RegisterEnergySensorMethods() {}
        protected virtual void RegisterNoiseSensorMethods() {}

    }
}