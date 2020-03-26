using BT.Compute.Bases.ComputeItems;
using UnityEngine;

namespace BT.Compute.Floats
{
    public class BTTimeNow : BTGetBase<float>
    {
        public override float GetValue()
        {
            return Time.time;
        }
    }
}
