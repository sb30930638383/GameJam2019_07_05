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
}
