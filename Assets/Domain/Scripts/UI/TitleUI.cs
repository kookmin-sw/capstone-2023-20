using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TitleUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyUI;
    [SerializeField]
    private GameObject titleUI;

    private void Awake()
    {
        if (PhotonNetwork.InLobby) OnJoinedLobby();
    }
    public void OnClickOnlineBtn()
    {
        Debug.Log("온라인 버튼 클릭");
        PhotonNetwork.ConnectUsingSettings();
    }

   

    public void ClickExitBtn()
    {
        Debug.Log("Click Exit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("온라인 연결");
        PhotonNetwork.JoinLobby();
     
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 참가 완료");
        titleUI.SetActive(false);
        lobbyUI.SetActive(true);
    }
}
