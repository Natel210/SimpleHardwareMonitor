using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.ItemList
{
    internal abstract class AHardwareGroup<TModel, THardwareNode> where TModel : struct where THardwareNode : HardwareNode.AHardwareNode<TModel>
    {
        internal Dictionary<string, TModel> ModelGroup { get => GetModelGroup(); }
        protected Dictionary<string, THardwareNode> _nodeGroup = new Dictionary<string, THardwareNode>();
        private Mutex _nodeGroupMutex = new Mutex();

        internal void AddNodeGroup(IHardware hardware)
        {
            lock (_nodeGroupMutex)
            {
                AddNodeGroupChild(hardware);
            }
        }

        protected abstract void AddNodeGroupChild(IHardware hardware);

        internal void Clear()
        {
            lock (_nodeGroupMutex)
                _nodeGroup.Clear();
        }

        internal bool Update(IHardware hardware)
        {
            lock (_nodeGroupMutex)
            {
                if (_nodeGroup.TryGetValue(hardware.Name, out var item))
                    return item.Update(hardware);
            }
            return false;
        }

        private Dictionary<string, TModel> GetModelGroup()
        {
            return GetNodeGroupChild();
        }

        protected abstract Dictionary<string, TModel> GetNodeGroupChild();
    }
}
