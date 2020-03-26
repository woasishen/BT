using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MonoExtensions
{
    public static string GetFullName(this Transform trsf)
    {
        return string.Join("-", trsf.GetParentList().Select(s => s.name));
    }

    public static List<Transform> GetParentList(this Transform transform)
    {
        var list = new List<Transform>();
        var trsf = transform;
        while (trsf)
        {
            list.Add(trsf);
            trsf = trsf.parent;
        }
        return list;
    }
}