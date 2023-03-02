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
        PhotonNetwork.AutomaticallySyncScene = true;
        CreatePlayer(PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    private void CreatePlayer(string nickName)
    {
        if (nickName.Equals("Latifa"))
        {
            Transform point = GameObject.Find("SpwanPointLatifa").GetComponent<Transform>();
            PhotonNetwork.Instantiate("Latifa", point.position, point.rotation);
 
        }
        else if (nickName.Equals("Taichi"))
        {
            
            Transform point = GameObject.Find("SpwanPointTaichi").GetComponent<Transform>();
            PhotonNetwork.Instantiate("Taichi", point.position, point.rotation);
        }
        else
        {
            Transform point = GameObject.Find("spwan").GetComponent<Transform>();
            PhotonNetwork.Instantiate("Lafita", point.position, point.rotation);
        }
        Debug.Log("ĳ���� ����" + nickName);
    }

 
}
