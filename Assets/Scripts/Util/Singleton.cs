using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static readonly object SynObject = new object();

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (SynObject)
                {
                    if (instance == null)
                        instance = new T();

                }
            }

            return instance;
        }
    }

    //private Singleton()
    //{

    //}

}