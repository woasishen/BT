using System.Linq;
using BT.Compute.Bases.ComputeItems;
using Sirenix.OdinInspector;

namespace Examples.BTs.GetComputes
{
    public class GetEnemyAttr<T> : BTGetBase<T>
    {
#if UNITY_EDITOR
        public ValueDropdownList<string> EnemyAttrStrs
        {
            get
            {
                var fields = EnemyAttr.AllFields.Where(
                    s => s.Value.PropertyType == typeof(T)).ToList();
                var result = new ValueDropdownList<string>();
                foreach (var field in fields)
                {
                    result.Add(field.Key);
                }
                return result;
            }
        }
#endif

        [ValueDropdown("EnemyAttrStrs")]
        public string EnemyAttrType;

        public override T GetValue()
        {
            var aiRoot = (AIBTRoot)Root;
            return (T)aiRoot.Enemy.EnemyAttr.GetValue(EnemyAttrType);
        }
    }
}
