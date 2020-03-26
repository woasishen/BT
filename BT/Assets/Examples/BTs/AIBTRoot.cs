using BT;

namespace Examples.BTs
{
    public class AIBTRoot : BTRoot
    {
        public Enemy Enemy { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Enemy = transform.GetComponentInParent<Enemy>();
        }
    }
}
