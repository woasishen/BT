using System;
using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector;

namespace BT.Compute.Bools
{
    [Serializable]
    public class BTBooExecuteData
    {
        [LabelText("A=B执行:")]
        public int EqualIndex = 0;
        [LabelText("A!=B执行:")]
        public int NotEqualIndex = 1;
    }

    [Serializable]
    public class BTBoolResultData
    {
        [LabelText("A=B返回:")]
        public BTState EqualState = BTState.Success;
        [LabelText("A!=B返回:")]
        public BTState NotEqualState = BTState.Failure;
    }

    [BTNamePre("Bool比较")]
    public class BTBoolCompare : BTCompareBase<bool,
        BTBooExecuteData,
        BTBoolResultData>
    {
        protected override int GetExecuteIndex()
        {
            return DataA == DataB
                ? ExecuteData.EqualIndex
                : ExecuteData.NotEqualIndex;
        }

        protected override BTState GetReturnState()
        {
            return DataA == DataB
                ? ResultData.EqualState
                : ResultData.NotEqualState;
        }
    }
}
