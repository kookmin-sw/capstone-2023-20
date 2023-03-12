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
        //마스터 클라이언트의 씬레벨 자동 동기화
        //PhotonNetwork.AutomaticallySyncScene = true;
        pv.RPC("CreatePlayer", RpcTarget.All,PhotonNetwork.LocalPlayer.NickName);
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties.ToString());
    }
    [PunRPC]
    private void CreatePlayer(string nickName)
    {
        //플레이어 프리팹 생성 리팩토링(2주차)
        Transform point = GameObject.Find("SpwanPoint" + nickName).GetComponent<Transform>();
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"])
        {
            PhotonNetwork.Instantiate("Player"+nickName, point.position, point.rotation);
            Debug.Log("캐릭터 생성" + "Player"+nickName);
        }
        /*
        if (nickName.Equals("Latifa"))
        {
            Transform point = GameObject.Find("SpwanPointLatifa").GetComponent<Transform>();
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"])
            {
                PhotonNetwork.Instantiate("PlayerLatifa", point.position, point.rotation);
                Debug.Log("캐릭터 생성" + nickName);
            }

        }
        else if (nickName.Equals("Taichi"))
        {
            Transform point = GameObject.Find("SpwanPointTaichi").GetComponent<Transform>();
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"])
            {
                PhotonNetwork.Instantiate("PlayerTaichi", point.position, point.rotation);
                Debug.Log("캐릭터 생성" + nickName);
            }
        }
        */
        
    }

 
}
