namespace BT.Tasks.Parallels
{
    /// <summary>
    /// 并行执行
    /// 有一个失败返回失败
    /// 全部成功返回成功
    /// </summary>
    [BTNamePre("并行-失败返回")]
    public class BTParallelOneFailure : BTTask
    {
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
            BTTask finishTask = null;
            foreach (var childTask in ChildTasks)
            {
                var state = childTask.__BTUpdate();
                if (state == BTState.Running)
                {
                    hasRunning = true;
                    continue;
                }

                if (state == OneFinishState)
                {
                    finishTask = childTask;
                }
            }

            if (finishTask)
            {
                foreach (var childTask in ChildTasks)
                {
                    if (childTask.State == BTState.Running)
                    {
                        childTask.State = BTState.ForceEnd;
                    }
                }
                return finishTask.State;
            }

            if (hasRunning)
            {
                return BTState.Running;
            }

            return AllFinishState;
        }
    }
}
