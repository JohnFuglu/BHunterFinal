using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance; 
    public static T Instance
    {
        get
        {
            if (_instance == null)//ici on peut faire lazy instanciation à la place du debug
                Debug.LogError(typeof(T).Name + "is Null !");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T; //ou (T)this
    }

    public virtual void Initialization() { }// peut être override si y a des choses à faire à l'ini
}
