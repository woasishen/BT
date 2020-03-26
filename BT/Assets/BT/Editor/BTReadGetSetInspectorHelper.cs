using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BT.Compute.Bases.ComputeItems;
using BT.Compute.Bases.ComputeTasks;
using Editor.EditorHelpers;
using UnityEditor;
using UnityEngine;

namespace BT.Editor
{
    public static class BTReadGetSetInspectorHelper
    {
        public static void FitGetSet(IBTReadGetSet target, SerializedObject serializedObject)
        {
            AdaptCompute(target, "Get", target.ReadGetCount, serializedObject);
        }

        private static char GetName(int index)
        {
            return (char)(index + 'A');
        }

        private static void AdaptCompute(IBTReadGetSet cb, 
            string pre, int computeCount, SerializedObject serializedObject)
        {
            if (computeCount == 0)
            {
                return;
            }
            var computeType = GetIComputeType(cb.GetType(), $"{pre}A");
            if (computeType == null)
            {
                Debug.LogError($"Err:No {pre}A Property");
                return;
            }
            var computes = ((MonoBehaviour)cb).GetComponents(computeType);
            var allIds = new List<string>();
            for (var index = 0; index < computes.Length; index++)
            {
                if (ReferenceEquals(computes[index] , cb))
                {
                    continue;
                }
                var compute = (IBTID)computes[index];
                if (string.IsNullOrEmpty(compute.ID) || allIds.Contains(compute.ID))
                {
                    compute.ID = index.ToString();
                }
                allIds.Add(compute.ID);
            }

            var allChooseIndex = new List<int>();
            for (var i = 0; i < computeCount; i++)
            {
                var id = serializedObject.FindProperty($"{pre}Id" + GetName(i));
                allChooseIndex.Add(allIds.IndexOf(id.stringValue));
            }

            for (var i = 0; i < computeCount; i++)
            {
                var id = serializedObject.FindProperty($"{pre}Id" + GetName(i));
                var chooseIndex = allChooseIndex[i];
                var valid = chooseIndex >= 0 
                            && allChooseIndex.Count(s => s == chooseIndex) == 1;

                ShowPopup($"{pre} {GetName(i)} ID:", 
                    valid, ref chooseIndex, id, allIds, serializedObject);
            }
        }

        private static void ShowPopup(string title, bool valid,
            ref int chooseIndex, SerializedProperty property, List<string> allIds,
            SerializedObject serializedObject)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(title),
                valid ? InspectorHelper.LabelNormalStyle : InspectorHelper.LabelErrStyle,
                GUILayout.Width(100f));
            var rectB = EditorGUILayout.GetControlRect(true);
            EditorGUI.BeginChangeCheck();
            chooseIndex = EditorGUI.Popup(rectB, chooseIndex, allIds.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = allIds[chooseIndex];
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
        }

        private static Type GetIComputeType(Type cb, string properName)
        {
            while (cb != null && cb != typeof(BTReadGetSetTaskBase))
            {
                var computeProperty = cb.GetProperty(properName,
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (computeProperty != null)
                {
                    return computeProperty.PropertyType;
                }

                cb = cb.BaseType;
            }
            return null;
        }
    }
}
