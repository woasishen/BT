using System;
using BT.Compute.Bases.ComputeItems;
using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BT.Tasks.Common
{
    [BTNamePre("等待")]
    public sealed class BTWaiting : BTReadGetSetTaskBase
    {
        [LabelText("使用固定时间")]
        public bool UseConstDuration = true;

        public override int ReadGetCount => UseConstDuration ? 0 : 1;

        [HideInInspector]
        public string GetIdA;
        private IBTGet<float> GetA { set; get; }

        [ShowIf("ReadGetCount", 0)]
        [SerializeField]
        private float _waitTime = 0;
        private float WaitTime => ReadGetCount == 1 ? GetA.GetValue(): _waitTime;

        [LabelText("等待后:")]
        public BTCompareType BTCompareResultType;
        [ShowIf("BTCompareResultType", BTCompareType.ReturnResult)]
        public BTState FinishState = BTState.Success;

        private float _endTime;
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

            if (ReadGetCount == 1 && GetA == null)
            {
                throw new Exception("Compute 不能为null");
            }
        }
        protected override void BTStart()
        {
            base.BTStart();
            _endTime = Time.time + WaitTime;
            _curTask = null;
        }

        protected override BTState BTUpdate()
        {
            if (Time.time < _endTime)
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
