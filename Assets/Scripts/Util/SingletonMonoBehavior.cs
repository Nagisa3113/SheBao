using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;

public class SingletonMonoBehavior<T> :SerializedMonoBehaviour where T : SingletonMonoBehavior<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = null;

                GameObject[] obj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject child in obj)
                {
                    //Debug.Log(child.gameObject.name);
                    if (child.name == typeof(T).Name)
                    {
                        gameObject = child;
                        if (gameObject.GetComponent<T>() == null)
                        {
                            gameObject.AddComponent<T>();
                        }
                    }
                }
                if (gameObject == null)
                {
                    gameObject = new GameObject(typeof(T).Name);
                    gameObject.AddComponent<T>();
                }

                DontDestroyOnLoad(gameObject);
                instance = gameObject.GetComponent<T>();
            }

            return instance;
        }
    }

}
