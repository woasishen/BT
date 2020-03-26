namespace BT.Tasks.Orders
{
    [BTNamePre("顺序-成功返回")]
    public class BTOrderOneSucceed : BTOrderOneFailure
    {
        protected override void BTAwake()
        {
            base.BTAwake();
            OneFinishState = BTState.Success;
            AllFinishState = BTState.Failure;
        }
    }
}
