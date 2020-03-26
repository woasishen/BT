using TMPro;
using UnityEditor;
using UnityEngine;

namespace BT
{
    public abstract class BTTask : BTBase
    {
        public override BTTask[] ChildTasks => _childTasks;
        public override bool ShowGUI => Root.ShowGUI;

        public override BTRoot Root
        {
            get
            {
                if (!_root)
                {
                    _root = GetComponentInParent<BTRoot>();
                }

                return _root;
            }
        }

        public BTState State { set; get; }
        public BTBase Parent { private set; get; }
        public int Index { get; private set; }

        private BTTask[] _childTasks;
        private BTRoot _root;

        public void __BTReset()
        {
            if (Root.LogInfo)
            {
                Debug.Log("BTReset" + name);
            }
            BTReset();
        }

        public void __BTAwake(BTRoot btRoot, int index)
        {
            _root = btRoot;
            Index = index;
            if (Root.LogInfo)
            {
                Debug.Log("BTAwake" + name);
            }

            Parent = transform.parent.GetComponent<BTBase>();
            _childTasks = new BTTask[transform.childCount];
            for (var i = 0; i < transform.childCount; i++)
            {
                ChildTasks[i] = transform.GetChild(i).GetComponent<BTTask>();
            }
            BTAwake();
        }

        public void __BTStart()
        {
            if (Root.LogInfo)
            {
                Debug.Log("BTStart" + name);
            }
            BTStart();
        }

        public BTState __BTUpdate()
        {
            if (Root.LogInfo)
            {
                Debug.Log("BTUpdate" + name);
            }
            State = BTUpdate();
            return State;
        }

        public virtual BTTask NextBt => Parent.ChildTasks.Length > Index + 1
            ? Parent.ChildTasks[Index + 1]
            : null;

        protected virtual void BTReset()
        {
            State = BTState.Waiting;
        }
        protected virtual void BTAwake() { }
        protected virtual void BTStart() { }
        protected virtual BTState BTUpdate()
        {
            return BTState.Success;
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            var text = GetComponent<TextMeshPro>();
            if (!text)
            {
                return;
            }
            base.OnDrawGizmos();
            if (EditorApplication.isPlaying)
            {
                switch (State)
                {
                    case BTState.Waiting:
                        text.color = Color.white;
                        break;
                    case BTState.Running:
                        text.color = Color.blue;
                        break;
                    case BTState.Success:
                        text.color = Color.green;
                        break;
                    case BTState.Failure:
                        text.color = Color.red;
                        break;
                    case BTState.ForceEnd:
                        text.color = Color.magenta;
                        break;
                }
            }
        }
        #endif
    }
}
