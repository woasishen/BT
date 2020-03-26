using UnityEngine;

namespace BT.Compute.Bases.ComputeTasks
{
    public interface IBTReadGetSet
    {
        int ReadGetCount { get; }
    }

    public abstract class BTReadGetSetTaskBase : BTTask, IBTReadGetSet
    {
        public abstract int ReadGetCount { get; }
    }

    public abstract class BTReadGetSetComputeBase : MonoBehaviour, IBTReadGetSet
    {
        public abstract int ReadGetCount { get; }
    }
}
