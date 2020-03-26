namespace BT.Compute.Bases.ComputeItems
{
    public interface IBTSet<in T>
    {
        void SetValue(T value);
    }
}