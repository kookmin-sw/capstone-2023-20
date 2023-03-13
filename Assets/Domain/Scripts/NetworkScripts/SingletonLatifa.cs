using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SingletonLatifa : MonoBehaviour
{
    private static SingletonLatifa instance;
    public static SingletonLatifa Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SingletonLatifa>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = new GameObject().AddComponent<SingletonLatifa>();
                }
            }
            return instance;
        }

    }

    //±è±â¹ü - °´Ã¼ Áßº¹°Ë»ç
    private void Awake()
    {
        var objs = FindObjectsOfType<SingletonLatifa>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
