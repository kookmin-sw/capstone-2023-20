using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    static int stage;
    private PhotonView pv;
    [SerializeField]
    private TMP_Text stateText;
    private void Awake()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties
            (new Hashtable { { "InGame", false } });
        PhotonNetwork.LocalPlayer.SetCustomProperties
            (new Hashtable { { "GameReady", false } });
    }
    private void Update()
    {
        
    }

    public void OnClickRestart()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties
    (new Hashtable { { "GameReady", true } });

        bool gameStart = true;
        foreach (int id in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            if (!(bool)PhotonNetwork.CurrentRoom.Players[id].CustomProperties["GameReady"]) gameStart = false;
        }
        if (gameStart)
        {
            Debug.Log("게임스타트");
            pv.RPC("SetState", RpcTarget.All, "<color=yellow>스테이지 재도전 준비중</color>");
            Hashtable rp = PhotonNetwork.CurrentRoom.CustomProperties;
            rp["InGame"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(rp);
            pv.RPC("Load", RpcTarget.All);
            LoadingSceneController.LoadScene(stage);
        }
        else
        {
            if (PhotonNetwork.LocalPlayer.NickName.Equals("Latifa")) SetStateText("예담이를 기다리는 중..");
            else SetStateText("승연이를 기다리는 중..");
        }
    }


    public static void LoadGameOver(int arg)
    {
        stage = arg;
        PhotonNetwork.LoadLevel("GameOver");
    }

    [PunRPC]
    void SetStateText(string arg)
    {
        stateText.text = arg;
    }


}
