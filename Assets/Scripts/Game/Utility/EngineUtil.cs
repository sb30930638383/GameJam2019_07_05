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
}
