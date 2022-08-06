using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public static class ExtensionMethods
{
    public static void ForEach<T>(this IEnumerable<T> enumator, Action<T> callback)
    {
        foreach (T element in enumator) { callback(element); }
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        if (attributes != null && attributes.Any()) return attributes.First().Description;
        return value.ToString();
    }

    public static void RenderSprite(Transform parent, string spriteName, string componentName) {
        Sprite sprite = Resources.LoadAll<Sprite>("UI_Atlas").Single(sprite => sprite.name.Equals(spriteName));
        SpriteRenderer renderer = parent.Find(componentName).gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }
}