using UnityEngine;

namespace BT.Compute.Bases.ComputeTasks
{
    public abstract class BTSwitchBase : BTTask
    {
        protected abstract int GetExecuteIndex();

        private BTTask _exeChildTask;
        protected override void BTStart()
        {
            base.BTStart();
            var index = GetExecuteIndex();
            if (ChildTasks.Length <= index)
            {
                Debug.LogError("No exe index");
                return;
            }
            _exeChildTask = ChildTasks[index];
            _exeChildTask.__BTStart();
            foreach (var childTask in ChildTasks)
            {
                if (childTask != _exeChildTask)
                {
                    childTask.State = BTState.ForceEnd;
                }
            }
        }

        protected override BTState BTUpdate()
        {
            if (!_exeChildTask)
            {
                return BTState.Failure;
            }
            return _exeChildTask.__BTUpdate();
        }
    }
}
