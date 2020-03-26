using BT;

namespace Examples.BTs
{
    public class AITaskBase : BTTask
    {
        protected AIBTRoot AIRoot => (AIBTRoot) Root;
        protected Enemy Enemy => ((AIBTRoot) Root).Enemy;
    }
}
