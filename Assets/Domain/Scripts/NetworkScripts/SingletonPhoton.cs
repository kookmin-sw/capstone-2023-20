using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� - ���ӸŴ��� �̱������� �����ϴ� Ŭ����
public class SingletonPhoton : MonoBehaviour
{
    private static SingletonPhoton instance;

    public static SingletonPhoton Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SingletonPhoton>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = new GameObject().AddComponent<SingletonPhoton>();
                }
            }
            return instance;
        }

    }

    //���� - ��ü �ߺ��˻�
    private void Awake()
    {
        var objs = FindObjectsOfType<SingletonPhoton>();
        if(objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
