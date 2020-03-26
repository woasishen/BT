using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BT
{
    [BTNamePre("BT")]
    public class BTRoot : BTBase
    {
        public override BTRoot Root => this;
        public override BTTask[] ChildTasks => _childTasks;
        public override bool ShowGUI => m_ShowGUI;
        public bool m_ShowGUI = false;
        [SerializeField]
        private float _updateDelta = 0;
        [SerializeField]
        private bool _repeatAlways = false;

        private BTTask[] _childTasks;
        private float _lastUpdateTime;

        public bool LogInfo;
        public BTTask CurItem { set; get; }

        protected virtual void Awake()
        {
            _childTasks = new BTTask[transform.childCount];
            if (ChildTasks.Length == 0)
            {
                return;
            }
            for (var i = 0; i < transform.childCount; i++)
            {
                ChildTasks[i] = transform.GetChild(i).GetComponent<BTTask>();
                AwakeTaskAndChildren(ChildTasks[i], i);
            }
            CurItem = ChildTasks[0];

#if !UNITY_EDITOR
            m_ShowGUI = false;
            DestroyGUIAll(transform);
#endif
        }

        protected virtual void Start()
        {
            if (!CurItem)
            {
                return;
            }
            DoWithAllTask(task => task.__BTReset());
            CurItem.__BTStart();
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
#endif
            if (ChildTasks.Length == 0)
            {
                return;
            }
            if (Time.time - _lastUpdateTime < _updateDelta)
            {
                return;
            }
            _lastUpdateTime = _updateDelta;
            if (!CurItem)
            {
                if (!_repeatAlways)
                {
                    if (!ShowGUI)
                    {
                        gameObject.SetActive(false);
                    }
                    return;
                }
                DoWithAllTask(task=>task.__BTReset());
                CurItem = ChildTasks[0];
                CurItem.__BTStart();
            }
            while (true)
            {
                var state = CurItem.__BTUpdate();
                if (state == BTState.Running)
                {
                    break;
                }
                CurItem = CurItem.NextBt;
                if (!CurItem)
                {
                    break;
                }
                CurItem.__BTStart();
            }
        }

        private void AwakeTaskAndChildren(BTTask task, int index)
        {
            task.__BTAwake(this, index);
            for (var i = 0; i < task.ChildTasks.Length; i++)
            {
                AwakeTaskAndChildren(task.ChildTasks[i], i);
            }
        }

#if UNITY_EDITOR

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

        protected override void OnDrawGizmos()
        {
            if (!Root.GetComponent<RectTransform>())
            {
                return;
            }
            var allTrsfWidth = new Dictionary<Transform, float>();
            var width = Root.GetComponent<RectTransform>().rect.width;
            ComputeAllChildWidth(transform, ref width, ref allTrsfWidth);

            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            FitAllChildPos(transform, allTrsfWidth);

            DrawLine(transform);
        }

        private void FitAllChildPos(Transform trsf, 
            Dictionary<Transform, float> allTrsfWidth)
        {
            var startX = -allTrsfWidth[trsf] / 2f;
            var y = -3f;

            for (var i = 0; i < trsf.childCount; i++)
            {
                var childTrsf = trsf.GetChild(i);
                startX += allTrsfWidth[childTrsf] / 2f;
                childTrsf.localPosition = new Vector3(startX, y);
                startX += allTrsfWidth[childTrsf] / 2f;
                FitAllChildPos(childTrsf, allTrsfWidth);
            }
        }

        private float ComputeAllChildWidth(Transform trsf, 
            ref float baseWidth, ref Dictionary<Transform, float> result)
        {
            if (trsf.childCount == 0)
            {
                result[trsf] = baseWidth;
                return result[trsf];
            }

            var selfW = 0f;
            for (var i = 0; i < trsf.childCount; i++)
            {
                selfW += ComputeAllChildWidth(trsf.GetChild(i), 
                    ref baseWidth, ref result);
            }
            result[trsf] = selfW;
            return result[trsf];
        }

        private void DrawLine(Transform trsf)
        {
            if (!ShowGUI)
            {
                return;
            }
            for (var i = 0; i < trsf.childCount; i++)
            {
                var childTrsf = trsf.GetChild(i);
                Debug.DrawLine(trsf.position + new Vector3(0, -0.5f, 0),
                    childTrsf.position + new Vector3(0, 0.5f, 0),
                    Color.green);
                DrawLine(childTrsf);
            }
        }

#endif
    }
}
