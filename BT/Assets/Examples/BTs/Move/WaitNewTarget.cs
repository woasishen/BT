using BT;

namespace Examples.BTs.Move
{
    public class WaitNewTarget : AITaskBase
    {
        protected override BTState BTUpdate()
        {
            if (!Enemy.EnemyAttr.NewTarget)
            {
                return BTState.Running;
            }

            return BTState.Success;
        }
    }
}
