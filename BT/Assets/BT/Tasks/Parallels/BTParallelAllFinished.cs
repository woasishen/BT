namespace BT.Tasks.Parallels
{
    /// <summary>
    /// 等待所有Running状态执行完成
    /// 返回第一个执行的结果
    /// </summary>
    [BTNamePre("并行-执行")]
    public class BTParallelAllFinished : BTTask
    {
        protected override void BTStart()
        {
            base.BTStart();
            foreach (var childTask in ChildTasks)
            {
                childTask.__BTStart();
            }
        }

        protected override BTState BTUpdate()
        {
            if (ChildTasks.Length == 0)
            {
                return BTState.Success;
            }

            var hasRunning = false;
            foreach (var childTask in ChildTasks)
            {
                var state = childTask.__BTUpdate();
                if (state == BTState.Running)
                {
                    hasRunning = true;
                }
            }

            if (hasRunning)
            {
                return BTState.Running;
            }

            return ChildTasks[0].State;
        }
    }
}
