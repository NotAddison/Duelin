using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static void ForEach<T>(this IEnumerable<T> enumator, Action<T> callback)
    {
        foreach (T element in enumator) { callback(element); }
    }
}