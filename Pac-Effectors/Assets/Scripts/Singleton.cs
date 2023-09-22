using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    static T _singleton;

    public static T singleton
    {
        get
        {
            if (!_singleton)
            {
                T potentialSingleton = FindObjectOfType<T>();
                if (potentialSingleton == null)
                {
                    //_singleton = FindObjectOfType<T>();
                    GameObject obj = new GameObject();
                    //obj.name = typeof(T).Name;
                    _singleton = obj.AddComponent<T>();
                }
                else
                {
                    _singleton = potentialSingleton;
                }
            }
            return _singleton;
        }
    }

    protected virtual void Awake()
    {
        if (!_singleton)
        {
            _singleton = this as T;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
