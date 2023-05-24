using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class ObjectManager : MonoBehaviour
{
    public UnityEvent FirstEvent;
    public UnityEvent Event;
    public UnityEvent OncePlayEvent;

    public UnityEvent SyncEvent;
    public UnityEvent SyncEvent2;
    public PhotonView pv;

    int PlayCount = 0;
    int FirstCount = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void SyncActivate()
    {
        if (SyncEvent.GetPersistentEventCount() > 0)
            SyncEvent.Invoke();
    }
    [PunRPC]
    public void SyncActivate2()
    {
        if (SyncEvent2.GetPersistentEventCount() > 0)
            SyncEvent2.Invoke();
    }

    public void SyncFunc()
    {
        pv.RPC("SyncActivate", RpcTarget.All);
    }

    public void Activate()
    {
        if (FirstEvent.GetPersistentEventCount() > 0  && FirstCount == 0)
        {
            FirstEvent.Invoke();
            FirstCount++;
        
        }
        else
        {
            if (PlayCount == 0)
            {
                OncePlayEvent.Invoke();
                PlayCount++;
            }
            Event.Invoke();
        }

        //if (pv != null)
        //    pv.RPC("SyncActivate", RpcTarget.Others);
    }




}
