using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;

public class SingletonMonoBehavior<T> : SerializedMonoBehaviour where T : SingletonMonoBehavior<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = GameObject.Find(typeof(T).ToString()) as GameObject;

                if (gameObject == null)
                {
                    gameObject = new GameObject(typeof(T).Name);
                    if (gameObject.GetComponent<T>() == null)
                    {
                        gameObject.AddComponent<T>();
                    }
                }

                DontDestroyOnLoad(gameObject);
                instance = gameObject.GetComponent<T>();
            }

            return instance;
        }
    }

}
