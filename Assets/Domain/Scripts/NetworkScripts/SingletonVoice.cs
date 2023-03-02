using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� -  �̱������� �����ϴ� Ŭ����
public class SingletonVoice : MonoBehaviour
{
    private static SingletonVoice instance;

    public static SingletonVoice Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SingletonVoice>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = new GameObject().AddComponent<SingletonVoice>();
                }
            }
            return instance;
        }

    }

    //���� - ��ü �ߺ��˻�
    private void Awake()
    {
        var objs = FindObjectsOfType<SingletonVoice>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
