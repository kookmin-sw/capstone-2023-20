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
        //Screen.SetResolution(760, 480, true);
    }
    public void OnClickOnlineBtn()
    {
        Debug.Log("�¶��� ��ư Ŭ��");
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
        Debug.Log("�¶��� ����");
        PhotonNetwork.JoinLobby();
     
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� �Ϸ�");
        titleUI.SetActive(false);
        lobbyUI.SetActive(true);
    }
}
