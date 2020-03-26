using System;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace BT
{
    public enum BTState
    {
        Waiting,
        Running,
        Success,
        Failure,
        ForceEnd,
    }

    public class BTNamePreAttribute : Attribute
    {
        public BTNamePreAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    [DisallowMultipleComponent]
    public abstract class BTBase : MonoBehaviour
    {
        public abstract BTRoot Root { get; }
        public abstract BTTask[] ChildTasks { get; }
        public abstract bool ShowGUI { get; }

        protected void DoWithAllTask(Action<BTTask> handle, BTTask[] tasks = null)
        {
            tasks = tasks ?? ChildTasks;
            foreach (var task in tasks)
            {
                handle(task);
                DoWithAllTask(handle, task.ChildTasks);
            }
        }

        #if UNITY_EDITOR

        public void FitName()
        {
            var namePreAttr = GetType().GetCustomAttribute<BTNamePreAttribute>();

            var namePre = namePreAttr == null ? GetType().Name : namePreAttr.Name + "-";
            if (!name.StartsWith(namePre))
            {
                name = namePre;
            }
            var textMesh = GetComponent<TextMeshPro>();
            if (!textMesh)
            {
                return;
            }
            var text = name.Replace("-", Environment.NewLine);
            textMesh.text = text;
        }

        protected virtual void OnDrawGizmos()
        {
        }

#endif
    }
}
