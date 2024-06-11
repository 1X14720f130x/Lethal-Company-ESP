using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LC_Internal;
using UnityObject = UnityEngine.Object;

static partial class Extensions
{
#nullable enable
    [MethodImpl(MethodImplOptions.AggressiveInlining)]

    internal static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T?> array) where T : UnityEngine.Object
    {
        foreach (T? element in array)
        {
            if (element is null || element.Unfake() == null) continue;
            yield return element;
        }
    }
#nullable disable

}
