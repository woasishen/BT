using System;
using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector;

namespace BT.Compute.CommonCompare
{
    [Serializable]
    public class BTCommonExecuteData
    {
        [LabelText("A>B执行:")]
        public int GreaterIndex = 0;
        [LabelText("A<B执行:")]
        public int SmallerIndex = 1;
        [LabelText("A=B执行:")]
        public int EqualIndex = 2;
    }

    [Serializable]
    public class BTCommonResultData
    {
        [LabelText("A>B返回:")]
        public BTState GreaterState = BTState.Success;
        [LabelText("A<B返回:")]
        public BTState SmallerState = BTState.Failure;
        [LabelText("A=B返回:")]
        public BTState EqualState = BTState.Failure;
    }

    public abstract class BTCommonCompareBase<T> : BTCompareBase<T,
        BTCommonExecuteData, BTCommonResultData> where T : IComparable
    {
        protected virtual int Compare(T a, T b)
        {
            return a.CompareTo(b);
        }

        protected override int GetExecuteIndex()
        {
            var res = Compare(DataA, DataB);
            return res == 0
                ? ExecuteData.EqualIndex
                : res > 0
                    ? ExecuteData.GreaterIndex
                    : ExecuteData.SmallerIndex;
        }

        protected override BTState GetReturnState()
        {
            var res = Compare(DataA, DataB);
            return res == 0
                ? ResultData.EqualState
                : res > 0
                    ? ResultData.GreaterState
                    : ResultData.SmallerState;
        }
    }
}
