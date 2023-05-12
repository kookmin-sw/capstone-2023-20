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
    public PhotonView pv;

    int PlayCount = 0;
    int FirstCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //pv = GetComponentFind(NetworkManager)<PhotonView>();
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
