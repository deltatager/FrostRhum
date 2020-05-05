using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    public static bool IsInitialized => Instance != null;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("(Singleton<" + GetType() + ">) Tried to instantiate second instance");
            Destroy(gameObject);
        }
        else
        {
            Instance = (T) this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
