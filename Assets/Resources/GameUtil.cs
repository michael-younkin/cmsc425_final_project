using UnityEngine;
using System.Collections;

public static class GameUtil {

    public static GameObject SafeFind(string name)
    {
        var r = GameObject.Find(name);
        if (r == null)
        {
            Debug.LogError(string.Format("Unable to find GameObject with name \"{0}\".", name));
        }
        return r;
    }

    public static T SafeLoad<T>(string path) where T: UnityEngine.Object
    {
        var r = Resources.Load<T>(path);
        if (r == null)
        {
            Debug.LogError(string.Format("Unable to load resource with name \"{0}\".", path));
        }
        return (T)r;
    }

    public static GameManager GameManager
    {
        get
        {
            return SafeFind("GameManager").SafeGetComponent<GameManager>();
        }
    }
}
