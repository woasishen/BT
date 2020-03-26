namespace BT.Tasks.Common
{
    [BTNamePre("返回")]
    public class BTReturn : BTTask
    {
        public BTState ReturnState = BTState.Success;

        protected override BTState BTUpdate()
        {
            return ReturnState;
        }
    }
}
