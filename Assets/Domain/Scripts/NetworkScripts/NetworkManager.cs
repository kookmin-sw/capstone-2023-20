using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    private void Awake()
    {
        //������ Ŭ���̾�Ʈ�� ������ �ڵ� ����ȭ
        //PhotonNetwork.AutomaticallySyncScene = true;
        pv.RPC("CreatePlayer", RpcTarget.All,PhotonNetwork.LocalPlayer.NickName);
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties.ToString());
    }
    [PunRPC]
    private void CreatePlayer(string nickName)
    {
        //�÷��̾� ������ ���� �����丵(2����)
        Transform point = GameObject.Find("SpwanPoint" + nickName).GetComponent<Transform>();
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"])
        {
            PhotonNetwork.Instantiate("Player"+nickName, point.position, point.rotation);
            Debug.Log("ĳ���� ����" + "Player"+nickName);
        }
        /*
        if (nickName.Equals("Latifa"))
        {
            Transform point = GameObject.Find("SpwanPointLatifa").GetComponent<Transform>();
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"])
            {
                PhotonNetwork.Instantiate("PlayerLatifa", point.position, point.rotation);
                Debug.Log("ĳ���� ����" + nickName);
            }

        }
        else if (nickName.Equals("Taichi"))
        {
            Transform point = GameObject.Find("SpwanPointTaichi").GetComponent<Transform>();
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"])
            {
                PhotonNetwork.Instantiate("PlayerTaichi", point.position, point.rotation);
                Debug.Log("ĳ���� ����" + nickName);
            }
        }
        */
        
    }

 
}
