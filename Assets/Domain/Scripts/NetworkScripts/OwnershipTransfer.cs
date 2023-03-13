using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public void OwnershipTransferLocalPlayer(Player player)
    {
        if (this.gameObject.name.Equals("Player" + player.NickName)) this.GetComponent<PhotonView>().RequestOwnership();
        Debug.Log(this.gameObject.name.Equals("Player" + player.NickName));
    }
    public override void OnJoinedRoom()
    {
        OwnershipTransferLocalPlayer(PhotonNetwork.LocalPlayer);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != this.GetComponent<PhotonView>()) return;

        base.photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        throw new System.NotImplementedException();
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }
}
