using BT.Compute.Bases.ComputeTasks;
using UnityEditor;

namespace BT.Editor
{
    [CustomEditor(typeof(BTReadGetSetTaskBase), true)]
    public class BTReadGetSetTaskInspector : BTBaseInspector
    {
        public override void OnInspectorGUI()
        {
            BTReadGetSetInspectorHelper.FitGetSet((IBTReadGetSet)target, serializedObject);
            base.OnInspectorGUI();
        }
    }
}
