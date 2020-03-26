using UnityEngine;

namespace BT.Compute.Bases.ComputeItems
{
    public abstract class BTGetBase<T> : MonoBehaviour, IBTGet<T>
    {
        [SerializeField]
        private string _computeId;
        public string ID
        {
            get => _computeId;
            set => _computeId = value;
        }

        public BTRoot Root { get; set; }
        public abstract T GetValue();
    }
}
