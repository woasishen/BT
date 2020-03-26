namespace BT.Tasks.Common
{
    [BTNamePre("重复直到")]
    public class BTRepeatUntil : BTTask
    {
        public BTState TargetState = BTState.Success;

        private BTTask _curTask;
        protected override void BTStart()
        {
            base.BTStart();
            ReInit();
        }

        private void ReInit()
        {
            _curTask = ChildTasks[0];
            _curTask.__BTStart();
        }
        
        protected override BTState BTUpdate()
        {
            if (_curTask == null)
            {
                ReInit();
            }
            while (_curTask != null)
            {
                var state = _curTask.__BTUpdate();
                if (state == BTState.Running)
                {
                    return BTState.Running;
                }
                if (state == TargetState)
                {
                    return TargetState;
                }
                _curTask = _curTask.NextBt;
                _curTask?.__BTStart();
            }
            return BTState.Running;
        }

    }
}
