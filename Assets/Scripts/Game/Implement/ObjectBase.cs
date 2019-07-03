using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    public int Id { get { return id; } }
    protected int id;

    private static int dbId;

    private void Awake()
    {
        id = dbId++;
    }

    public static T FactoryCreate<T>() where T : ObjectBase
    {
        T t = new GameObject().AddComponent<T>();
        var name = string.Format("{0}_{1}", typeof(T).Name, t.id);
        t.name = name;
        return t;
    }
}
