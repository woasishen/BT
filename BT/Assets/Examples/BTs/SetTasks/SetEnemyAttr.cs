using System.Linq;
using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector;

namespace Examples.BTs.SetTasks
{
    public class SetEnemyAttr<T> : BTSetValueBase<T>
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

        public override void SetValue(T value)
        {
            var aiRoot = (AIBTRoot)Root;
            aiRoot.Enemy.EnemyAttr.SetValue(EnemyAttrType, value);
        }
    }
}
