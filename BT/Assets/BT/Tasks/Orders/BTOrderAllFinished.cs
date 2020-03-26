namespace BT.Tasks.Orders
{
    /// <summary>
    /// 顺序执行完所有子节点（无论子节点成功失败），返回指定结果
    /// </summary>
    [BTNamePre("顺序-执行")]
    public class BTOrderAllFinished : BTTask
    {
        private BTTask _curTask;
        public BTState ResultState = BTState.Success;
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
                    return ResultState;
                }

                var state = _curTask.__BTUpdate();
                if (state == BTState.Running)
                {
                    return BTState.Running;
                }
                _curTask = _curTask.NextBt;
                _curTask?.__BTStart();
            }
        }
    }
}
