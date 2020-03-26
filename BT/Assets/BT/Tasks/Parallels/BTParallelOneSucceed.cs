namespace BT.Tasks.Parallels
{
    /// <summary>
    /// 并行执行
    /// 有一个成功返回成功
    /// 全部失败返回失败
    /// </summary>
    [BTNamePre("并行-成功返回")]
    public class BTParallelOneSucceed : BTParallelOneFailure
    {
        protected override void BTAwake()
        {
            base.BTAwake();
            OneFinishState = BTState.Success;
            AllFinishState = BTState.Failure;
        }
    }
}
