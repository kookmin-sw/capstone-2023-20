using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using StarterAssets;

public class GameOverManager : MonoBehaviour
{
    static int stage;
    private PhotonView pv;
    [SerializeField]
    private TMP_Text stateText;

    private void Awake()
    { 
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
        if (cp.ContainsKey("InGame")) cp.Remove("InGame"); //충돌 방지 확실하게 삭제후 업데이트 하기 위함;
        if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
        cp.Add("InGame", false);
        cp.Add("GameOver", true); //게임오버상태인지 아닌지
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
        cp = PhotonNetwork.LocalPlayer.CustomProperties;
        if (cp.ContainsKey("GameReady")) cp.Remove("GameReady");
        cp.Add("GameReady", false);
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        pv = gameObject.GetPhotonView();

        //플레이어 캐릭터 파괴
        PhotonNetwork.Destroy(GameObject.Find("Player" + PhotonNetwork.LocalPlayer.NickName));

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
        stage = (int)PhotonNetwork.CurrentRoom.CustomProperties["CurrentLevel"];
        PhotonNetwork.LoadLevel("GameOver");

    }

    [PunRPC]
    void SetStateText(string arg)
    {
        stateText.text = arg;
    }


}
