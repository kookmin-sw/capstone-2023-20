using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//김기범 -  싱글톤임을 보장하는 클래스
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

    //김기범 - 객체 중복검사
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
