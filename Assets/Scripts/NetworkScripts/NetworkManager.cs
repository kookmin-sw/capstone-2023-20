using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        CreatePlayer(PhotonNetwork.LocalPlayer.NickName);
    }

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
        Debug.Log("캐릭터 생성" + nickName);
    }
}
