namespace BT.Compute.Bases.ComputeItems
{
    public abstract class BTConstBase<T> : BTGetBase<T>
    {
        public T Data;

        public override T GetValue()
        {
            return Data;
        }
    }
}
