using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


//김기범 - 게임매니저 싱글톤임을 보장하는 클래스
public class SingletonPhoton : MonoBehaviourPunCallbacks
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

    //김기범 - 객체 중복검사
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
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
