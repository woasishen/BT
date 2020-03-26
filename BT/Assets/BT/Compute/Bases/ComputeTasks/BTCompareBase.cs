using System;
using BT.Compute.Bases.ComputeItems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BT.Compute.Bases.ComputeTasks
{
    public enum BTCompareType
    {
        ExecuteChild,
        ReturnResult,
    }

    public abstract class BTCompareBase<T, T1, T2> : BTReadGetSetTaskBase where T : IComparable
    {
        [LabelText("使用一个常量")]
        public bool CompareConst = true;
        public override int ReadGetCount => CompareConst ? 1 : 2;

        [HideInInspector]
        public string GetIdA;
        [HideInInspector]
        public string GetIdB;

        private IBTGet<T> GetA { set; get; }
        private IBTGet<T> GetB { set; get; }

        [ShowIf("ReadGetCount", 1)]
        [SerializeField]
        private T _b = default;

        protected T DataA => GetA.GetValue();
        protected T DataB => ReadGetCount == 1? _b: GetB.GetValue();

        [LabelText("比较后:")]
        public BTCompareType BTCompareResultType;

        [ShowIf("BTCompareResultType", BTCompareType.ExecuteChild)]
        [SerializeField]
        public T1 ExecuteData;
        [ShowIf("BTCompareResultType", BTCompareType.ReturnResult)]
        [SerializeField]
        public T2 ResultData;

        protected abstract int GetExecuteIndex();
        protected abstract BTState GetReturnState();

        private BTTask _exeChildTask;

        protected override void BTAwake()
        {
            base.BTAwake();
            var computes = GetComponents<IBTGet<T>>();
            foreach (var compute in computes)
            {
                if (compute.ID == GetIdA)
                {
                    GetA = compute;
                    GetA.Root = Root;
                    continue;
                }

                if (compute.ID == GetIdB)
                {
                    GetB = compute;
                    GetB.Root = Root;
                }
            }

            if (GetA == null || ReadGetCount == 2 && GetB == null)
            {
                throw new Exception("Compute 不能为null" + transform.GetFullName());
            }
        }

        protected override void BTStart()
        {
            base.BTStart();
            switch (BTCompareResultType)
            {
                case BTCompareType.ExecuteChild:
                    ExecuteChildStart();
                    break;
                case BTCompareType.ReturnResult:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override BTState BTUpdate()
        {
            switch (BTCompareResultType)
            {
                case BTCompareType.ExecuteChild:
                    return ExecuteChildUpdate();
                case BTCompareType.ReturnResult:
                    return GetReturnState();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private BTState ExecuteChildUpdate()
        {
            if (!_exeChildTask)
            {
                return BTState.Failure;
            }
            return _exeChildTask.__BTUpdate();
        }

        private void ExecuteChildStart()
        {
            var index = GetExecuteIndex();
            if (ChildTasks.Length <= index)
            {
                Debug.LogError("No exe index");
                return;
            }
            _exeChildTask = ChildTasks[index];
            _exeChildTask.__BTStart();
            foreach (var childTask in ChildTasks)
            {
                if (childTask != _exeChildTask)
                {
                    childTask.State = BTState.ForceEnd;
                }
            }
        }
    }
}
