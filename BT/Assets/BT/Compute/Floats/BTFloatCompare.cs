using BT.Compute.CommonCompare;
using UnityEngine;

namespace BT.Compute.Floats
{
    [BTNamePre("Float比较")]
    public class BTFloatCompare : BTCommonCompareBase<float>
    {
        protected override int Compare(float src, float target)
        {
            if (Mathf.Approximately(src, target))
            {
                return 0;
            }
            return base.Compare(src, target);
        }
    }
}
