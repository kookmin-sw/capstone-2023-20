using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTaichi : MonoBehaviour
{
    private static SingletonTaichi instance;

    public static SingletonTaichi Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SingletonTaichi>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = new GameObject().AddComponent<SingletonTaichi>();
                }
            }
            return instance;
        }

    }

    //±è±â¹ü - °´Ã¼ Áßº¹°Ë»ç
    private void Awake()
    {
        var objs = FindObjectsOfType<SingletonTaichi>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
