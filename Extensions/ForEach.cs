using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityObject = UnityEngine.Object;

static partial class Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]

    internal static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
    {
        foreach (T item in array)
        {
            action(item);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]

    internal static void ForEach<T>(this IEnumerable<T> array, Action<int, T> action)
    {
        int i = 0;

        foreach (T item in array)
        {
            action(i++, item);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void ForEach<T>(this HashSet<T> array, Action<int, T> action)
    {
        int i = 0;

        foreach (T item in array)
        {
            action(i++, item);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]

    internal static void ForEach<T>(this T[] array, Action<int, T> action)
    {
        for (int i = 0; i < array.Length; i++)
        {
            action(i, array[i]);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]

    internal static void ForEach<T>(this List<T> array, Action<int, T> action)
    {
        for (int i = 0; i < array.Count; i++)
        {
            action(i, array[i]);
        }
    }


}
