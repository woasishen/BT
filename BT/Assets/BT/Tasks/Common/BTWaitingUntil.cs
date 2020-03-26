using System;
using BT.Compute.Bases.ComputeItems;
using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BT.Tasks.Common
{
    [BTNamePre("等待到指定时间")]
    public sealed class BTWaitingUntil : BTReadGetSetTaskBase
    {
        public override int ReadGetCount { get; } = 1;

        [HideInInspector]
        public string GetIdA;
        private IBTGet<float> GetA { set; get; }

        [LabelText("等待后:")]
        public BTCompareType BTCompareResultType;
        [ShowIf("BTCompareResultType", BTCompareType.ReturnResult)]
        public BTState FinishState = BTState.Success;

        private BTTask _curTask;

        protected override void BTAwake()
        {
            base.BTAwake();
            var computes = GetComponents<IBTGet<float>>();
            foreach (var compute in computes)
            {
                if (compute.ID == GetIdA)
                {
                    GetA = compute;
                    GetA.Root = Root;
                    break;
                }
            }

            if (GetA == null)
            {
                throw new Exception("Compute 不能为null");
            }
        }
        protected override void BTStart()
        {
            base.BTStart();
            _curTask = null;
        }

        protected override BTState BTUpdate()
        {
            if (Time.time < GetA.GetValue())
            {
                return BTState.Running;
            }

            switch (BTCompareResultType)
            {
                case BTCompareType.ExecuteChild:
                    if (!_curTask)
                    {
                        _curTask = ChildTasks[0];
                        _curTask.__BTStart();
                    }
                    return _curTask.__BTUpdate();
                case BTCompareType.ReturnResult:
                    return FinishState;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
