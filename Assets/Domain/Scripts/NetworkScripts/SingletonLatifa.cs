using System.Collections;
using System.Collections.Generic;
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

    //���� - ��ü �ߺ��˻�
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
