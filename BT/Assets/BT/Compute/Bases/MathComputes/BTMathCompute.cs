using System;
using BT.Compute.Bases.ComputeItems;
using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BT.Compute.Bases.MathComputes
{
    public enum BTMathComputeType
    {
        [LabelText("+")]
        Addition,
        [LabelText("-")]
        Subtraction,
        [LabelText("*")]
        Multiplication,
        [LabelText("/")]
        Division
    }

    public abstract class BTMathCompute<T> : BTReadGetSetComputeBase, IBTGet<T>
    {
        [SerializeField]
        private string _computeId;
        public string ID
        {
            get => _computeId;
            set => _computeId = value;
        }

        [LabelText("使用一个常量")]
        public bool CompareConst = true;
        public override int ReadGetCount => CompareConst ? 1 : 2;

        public BTRoot Root { get; set; }

        [HideInInspector]
        public string GetIdA;
        [HideInInspector]
        public string GetIdB;

        private IBTGet<T> GetA { set; get; }
        private IBTGet<T> GetB { set; get; }
        [ShowIf("ReadGetCount", 1)]
        [SerializeField]
        private T _b = default;

        private T DataA => GetA.GetValue();
        private T DataB => ReadGetCount == 1 ? _b : GetB.GetValue();
        public BTMathComputeType ComputeType;


        protected abstract T ComputeValue(T a, T b, BTMathComputeType computeType);

        public T GetValue()
        {
            return ComputeValue(DataA, DataB, ComputeType);
        }

        private void Awake()
        {
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
    }
}
