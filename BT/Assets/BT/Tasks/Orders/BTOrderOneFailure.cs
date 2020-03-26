namespace BT.Tasks.Orders
{
    [BTNamePre("顺序-失败返回")]
    public class BTOrderOneFailure : BTTask
    {
        private BTTask _curTask;

        protected BTState OneFinishState;
        protected BTState AllFinishState;

        protected override void BTAwake()
        {
            base.BTAwake();
            OneFinishState = BTState.Failure;
            AllFinishState = BTState.Success;
        }

        protected override void BTStart()
        {
            base.BTStart();
            if (ChildTasks.Length > 0)
            {
                _curTask = ChildTasks[0];
                _curTask.__BTStart();
            }
        }

        protected override BTState BTUpdate()
        {
            while (true)
            {
                if (_curTask == null)
                {
                    return AllFinishState;
                }
                var state = _curTask.__BTUpdate();
                if (state == BTState.Running)
                {
                    return BTState.Running;
                }
                if (state == OneFinishState)
                {
                    for (var i = _curTask.Index + 1; i < ChildTasks.Length; i++)
                    {
                        ChildTasks[i].State = BTState.ForceEnd;
                    }
                    return OneFinishState;
                }

                _curTask = _curTask.NextBt;
                _curTask?.__BTStart();
            }
        }
    }
}
