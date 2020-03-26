using UnityEngine;
using UnityEngine.AI;

namespace Examples
{
    public class Click : MonoBehaviourBase
    {
        public Enemy Enemy;

        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo))
                {
                    if (hitInfo.transform != transform)
                    {
                        return;
                    }
                    Enemy.EnemyAttr.TargetPos = hitInfo.point;
                }
            }
        }
    }
}
