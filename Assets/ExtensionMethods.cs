using UnityEngine;
using System.Collections;

public static class ExtensionMethods {
    public static T SafeGetComponent<T>(this Component obj) where T : Component
    {
        return obj.gameObject.SafeGetComponent<T>();
    }

    public static T SafeGetComponent<T>(this GameObject obj) where T : Component
    {
        var r = obj.GetComponent<T>();
        if (r == null)
        {
            Debug.LogError(string.Format("Unable to find Component of type \"{0}\".", typeof(T).ToString()));
        }
        return r;
    }

    public static Transform SafeFindChild(this Transform obj, string name)
    {
        Transform child = obj.Find(name);
        if (child == null)
        {
            Debug.LogError(string.Format("Unable to find GameObject child of \"{0}\" with name \"{0}\".", obj.name, name));
        }
        return child;
    }

    public static GameObject SafeFindChild(this GameObject obj, string name)
    {
        return obj.transform.SafeFindChild(name).gameObject;
    }
}
