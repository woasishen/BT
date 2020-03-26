
using UnityEngine;
using UnityEngine.AI;

namespace Examples
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviourBase
    {
        public NavMeshAgent Agent { get; private set; }

        public EnemyAttr EnemyAttr { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            EnemyAttr = new EnemyAttr(this);
        }
    }
}
