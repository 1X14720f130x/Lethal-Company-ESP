using System.Runtime.CompilerServices;
using UnityEngine;

namespace LC_Internal
{
    static partial class Extensions
    {
#nullable enable
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T? Unfake<T>(this T? obj) where T : Object => obj is null || obj.Equals(null) ? null : obj;
#nullable restore

    }
}
