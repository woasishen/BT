using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BT.Editor
{
    [CustomEditor(typeof(BTRoot), true)]
    public class BTRootInspector : BTBaseInspector
    {
        static BTRootInspector()
        {
            Selection.selectionChanged += SelectionChanged;
        }

        private static List<GameObject> _savedHideGameObjets = new List<GameObject>();
        private static bool _enteredBTRoot;
        private static void SaveHideState(Transform trsf)
        {
            if (SceneVisibilityManager.instance.IsHidden(trsf.gameObject))
            {
                _savedHideGameObjets.Add(trsf.gameObject);
            }

            for (var i = 0; i < trsf.childCount; i++)
            {
                SaveHideState(trsf.GetChild(i));
            }
        }

        private static void SelectionChanged()
        {
            if (Selection.activeGameObject?.GetComponent<BTRoot>())
            {
                if (!_enteredBTRoot)
                {
                    _enteredBTRoot = true;
                    _savedHideGameObjets.Clear();
                    for (var i = 0; i < SceneManager.sceneCount; i++)
                    {
                        var tmpS = SceneManager.GetSceneAt(i);
                        foreach (var go in tmpS.GetRootGameObjects())
                        {
                            SaveHideState(go.transform);
                        }
                    }
                }
                SceneVisibilityManager.instance.HideAll();
                SceneVisibilityManager.instance.Show(Selection.activeGameObject, true);
            }
            else
            {
                if (_enteredBTRoot)
                {
                    _enteredBTRoot = false;
                    SceneVisibilityManager.instance.ShowAll();
                    foreach (var go in _savedHideGameObjets)
                    {
                        SceneVisibilityManager.instance.Hide(go, false);
                    }
                    _savedHideGameObjets.Clear();
                }
            }
        }

        private static GUIStyle BtnStyle =>
            new GUIStyle("ButtonRight")
            {
                normal = { textColor = Color.blue },
                fontSize = 14,
            };

        public override void OnInspectorGUI()
        {
            var showGUI = serializedObject.FindProperty("m_ShowGUI");
            var btRoot = (BTRoot) target;
            InitTextMesh(btRoot);
            if (btRoot.ShowGUI)
            {
                if (GUILayout.Button("隐藏 UI", BtnStyle))
                {
                    showGUI.boolValue = false;
                    serializedObject.ApplyModifiedProperties();
                    btRoot.GetComponent<TextMeshPro>().enabled = false;
                    for (var i = 0; i < btRoot.transform.childCount; i++)
                    {
                        DestroyGUIAll(btRoot.transform.GetChild(i));
                    }
                }
            }
            else
            {
                if (GUILayout.Button("显示 UI", BtnStyle))
                {
                    showGUI.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                    btRoot.GetComponent<TextMeshPro>().enabled = true;
                    ShowGUIAll(btRoot);
                }
            }
            base.OnInspectorGUI();
        }

        private void InitTextMesh(BTRoot root)
        {
            if (root.GetComponent<TextMeshPro>())
            {
                return;
            }

            var text = root.gameObject.AddComponent<TextMeshPro>();
            var rect = root.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2.2f);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            text.enableAutoSizing = true;
            text.fontSizeMin = 0;
            text.fontSizeMax = 5;
            text.fontStyle = FontStyles.Bold;
            text.lineSpacing = -2f;
            text.alignment = TextAlignmentOptions.Midline;
        }

        private void ShowGUIAll(BTBase self)
        {
            CopyMeshFromRoot(self);
            self.FitName();
            for (var i = 0; i < self.transform.childCount; i++)
            {
                ShowGUIAll(self.transform.GetChild(i).GetComponent<BTBase>());
            }
        }

        private void DestroyGUIAll(Transform trsf)
        {
            DestroyImmediate(trsf.GetComponent<TextMeshPro>());
            DestroyImmediate(trsf.GetComponent<MeshRenderer>());
            DestroyImmediate(trsf.GetComponent<CanvasRenderer>());
            DestroyImmediate(trsf.GetComponent<MeshFilter>());
            for (var i = 0; i < trsf.childCount; i++)
            {
                DestroyGUIAll(trsf.GetChild(i));
            }
        }
    }
}
