using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EngineUtil
{
    public static void SetParentWithNormal(this Transform child, Transform parent)
    {
        if (child == null || parent == null) return;
        child.SetParent(parent);
        child.localPosition = Vector3.zero;
        child.rotation = Quaternion.identity;
        child.localScale = Vector3.one;
    }

    public static void SetTagWithChild(this Transform parent, string tag)
    {
        parent.tag = tag;
        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child;
                child = parent.GetChild(i);
                child.tag = tag;
                if (child.childCount > 0)
                    SetTagWithChild(child, tag);
            }
        }
    }

    public static Vector2 WorldPosToUIPos(Vector2 worldPos)
    {
        Vector2 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        return Camera.main.ViewportToWorldPoint(viewPos);
    }

    public static string ToStr(this Vector2 v2)
    {
        return string.Format("{0},{1}", v2.x, v2.y);
    }

    public static Vector2 ToV2(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return Vector2.zero;
        string[] arr = str.Split(',');
        Vector2 result;
        float.TryParse(arr[0], out result.x);
        float.TryParse(arr[1], out result.y);
        return result;
    }
}
