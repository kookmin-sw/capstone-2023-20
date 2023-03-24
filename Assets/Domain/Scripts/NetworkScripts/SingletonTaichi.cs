using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SingletonTaichi : MonoBehaviourPunCallbacks
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

    //김기범 - 객체 중복검사,pv할당
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
    //KKB - 방을 나가면 해당 객체 파괴
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
