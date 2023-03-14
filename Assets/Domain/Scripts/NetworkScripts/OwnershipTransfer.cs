using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    [SerializeField]
    private PhotonView pv;
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OwnershipTransferLocalPlayer(Player player)
    {
        
        Debug.Log("호출한 오브젝트 명 : " +this.gameObject.name);
        pv.RequestOwnership();

    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != pv) return;
        if (!this.gameObject.name.Equals("Player" + requestingPlayer.NickName)) return;
        Debug.Log("Onreq : " + PhotonNetwork.LocalPlayer.NickName + "req Pl :"  + requestingPlayer.NickName);
        //this.gameObject.GetComponent<PhotonView>().TransferOwnership(requestingPlayer);
        pv.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (targetView != pv) return;
        Debug.Log("Ontransfer : " + PhotonNetwork.LocalPlayer.NickName + "prev owner :" + previousOwner.NickName);
        //throw new System.NotImplementedException();
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        Debug.Log("Transfer failed");
        //throw new System.NotImplementedException();
    }
}
