using System;
using BT.Compute.Bases.ComputeItems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BT.Compute.Bases.ComputeTasks
{
    public abstract class BTSetValueBase<T> : BTReadGetSetTaskBase, IBTSet<T>
    {
        public abstract void SetValue(T value);

        [LabelText("使用常量赋值")]
        public bool CompareConst = true;
        public override int ReadGetCount => CompareConst ? 0 : 1;

        [HideInInspector]
        public string GetIdA;

        private IBTGet<T> GetA { set; get; }

        [ShowIf("ReadGetCount", 0)]
        [SerializeField]
        private T _b = default;

        protected T DataA => ReadGetCount == 0 ? _b : GetA.GetValue();

        protected override void BTAwake()
        {
            base.BTAwake();
            var gets = GetComponents<IBTGet<T>>();
            foreach (var compute in gets)
            {
                if (compute.ID == GetIdA)
                {
                    GetA = compute;
                    GetA.Root = Root;
                    break;
                }
            }

            if ( ReadGetCount == 1 && GetA == null)
            {
                throw new Exception("Compute 不能为null" + transform.GetFullName());
            }
        }

        protected override void BTStart()
        {
            base.BTStart();
            SetValue(DataA);
        }

        protected override BTState BTUpdate()
        {
            return BTState.Success;
        }
    }
}
