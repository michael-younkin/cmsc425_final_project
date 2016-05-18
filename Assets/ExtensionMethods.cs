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
}
