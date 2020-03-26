namespace BT.Compute.Bases.ComputeItems
{
    public interface IBTGet<out T> : IBTID
    {
        T GetValue();
        BTRoot Root { set; get; }
    }
}