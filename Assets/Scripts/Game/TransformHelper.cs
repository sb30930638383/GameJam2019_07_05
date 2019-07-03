using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper  {

	public  static GameObject FindChild(Transform parent,string childName)
    {
        var child = parent.Find(childName);
        if (child != null)
            return child.gameObject;
        
        GameObject go;
        for (int i = 0; i < parent.childCount; i++)
        {
            child = parent.GetChild(i);
            go = FindChild(child, childName);
            if (go != null)
            { return go; }                             
        }
        return null;
    }
}
