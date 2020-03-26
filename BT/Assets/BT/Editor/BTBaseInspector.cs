using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace BT.Editor
{
    [CustomEditor(typeof(BTBase), true)]
    public class BTBaseInspector : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            var btBase = (BTBase) target;
            if (btBase.ShowGUI)
            {
                CopyMeshFromRoot(btBase);
            }
            btBase.FitName();
            base.OnInspectorGUI();
        }
        
        protected void CopyMeshFromRoot(BTBase self)
        {
            if (self == self.Root || self.GetComponent<TextMeshPro>())
            {
                return;
            }
            var curText = self.gameObject.AddComponent<TextMeshPro>();
            UnityEditorInternal.ComponentUtility.CopyComponent(self.Root.GetComponent<TextMeshPro>());
            UnityEditorInternal.ComponentUtility.PasteComponentValues(curText);

            var curMr = self.GetComponent<MeshRenderer>();
            curMr.sharedMaterial = Instantiate(self.Root.GetComponent<MeshRenderer>().sharedMaterial);

            var curRect = self.GetComponent<RectTransform>();
            var oldPos = curRect.position;
            UnityEditorInternal.ComponentUtility.CopyComponent(self.Root.GetComponent<RectTransform>());
            UnityEditorInternal.ComponentUtility.PasteComponentValues(curRect);
            curRect.position = oldPos;
        }
    }
}
