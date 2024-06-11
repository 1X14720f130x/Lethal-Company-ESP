using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LC_Internal
{
    static partial class Helper
    {
#nullable enable
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T? FindObject<T>() where T : Component => UnityEngine.Object.FindAnyObjectByType<T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T[] FindObjects<T>(FindObjectsSortMode sortMode = FindObjectsSortMode.None) where T : Component =>
        UnityEngine.Object.FindObjectsByType<T>(sortMode);
#nullable disable
    }
}
