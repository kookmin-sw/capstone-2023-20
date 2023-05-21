using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using StarterAssets;
using ExitGames.Client.Photon;

public class GameOverManager : MonoBehaviourPunCallbacks
{
   
    [SerializeField]
    private PhotonView pv;
    [SerializeField]
    private TMP_Text stateText;


    public void OnClickRestart()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        if (cp.ContainsKey("GameReady")) cp.Remove("GameReady");
        cp.Add("GameReady", true);
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        bool gameStart = true;
        foreach (int id in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            if (!(bool)PhotonNetwork.CurrentRoom.Players[id].CustomProperties["GameReady"]) gameStart = false;
        }
        if (gameStart)
        {
            Debug.Log("게임스타트");
            pv.RPC("SetStateText", RpcTarget.All, "<color=yellow>스테이지 재도전 준비중</color>");
            Hashtable rp = PhotonNetwork.CurrentRoom.CustomProperties;
            rp["InGame"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(rp);
            pv.RPC("Load", RpcTarget.All);
        }
        else
        {
            if (PhotonNetwork.LocalPlayer.NickName.Equals("Latifa")) SetStateText("예담이를 기다리는 중..");
            else SetStateText("승연이를 기다리는 중..");
        }
    }

    [PunRPC]
    void Load()
    {
        Debug.Log("currentLevel at GameOver : " + PhotonNetwork.CurrentRoom.CustomProperties["CurrentLevel"]);
        if (PhotonNetwork.IsMasterClient) LoadingSceneController.LoadScene();
    }

    public static void LoadGameOver()
    {
        PhotonNetwork.LoadLevel("GameOver");
        
    }

    [PunRPC]
    void SetStateText(string arg)
    {
        stateText.text = arg;
    }


}
