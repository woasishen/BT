using BT.Compute.Bases.ComputeTasks;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace BT.Editor
{
    [CustomEditor(typeof(BTReadGetSetComputeBase), true)]
    public class BTReadGetSetComputeInspector : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            BTReadGetSetInspectorHelper.FitGetSet((IBTReadGetSet)target, serializedObject);
            base.OnInspectorGUI();
        }
    }
}
