using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class RoomUI : MonoBehaviourPunCallbacks//,IPunObservable
{
    [SerializeField]
    private GameObject lobbyUI;
    [SerializeField]
    private GameObject roomUI;
    [SerializeField]
    private TMP_Text RoomInfo;
    [SerializeField]
    private TMP_Text[] ChatLog;
    [SerializeField]
    private TMP_InputField ChatInput;
    [SerializeField]
    private GameObject LatifaRole;
    [SerializeField]
    private GameObject TaichiRole;
    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button LafitaSelectBtn;
    [SerializeField]
    private Button TaichiSelectBtn;

    public PhotonView pv;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && ChatInput.text != "")
        {
            OnClickSendChatBtn();
        }

        

    }
    public override void OnJoinedRoom()
    {
        lobbyUI.SetActive(false);
        roomUI.SetActive(true);
        Debug.Log("�� ����");
        RoomRenewal();

        string usingNick = ""; //�濡�� �̹� ���Ǵ� ĳ����
        foreach (int i in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            if (!PhotonNetwork.CurrentRoom.Players[i].Equals(PhotonNetwork.LocalPlayer)) usingNick = PhotonNetwork.CurrentRoom.Players[i].NickName;
        }
        foreach (int i in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            if (PhotonNetwork.CurrentRoom.Players[i].Equals(PhotonNetwork.LocalPlayer))
            {
                if (usingNick.Equals("Taichi"))
                {
                    PhotonNetwork.LocalPlayer.NickName = "Latifa";
                }
                else if (usingNick.Equals("Latifa"))
                {
                    PhotonNetwork.LocalPlayer.NickName = "Taichi";
                }
            }
        }
        ChatInput.text = "";
        for (int i = 0; i < ChatLog.Length; i++) ChatLog[i].text = "";
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2) ChatRPC("<color=red>" + "���� ���� ������ ���� �����ϼ���. �ΰ��ӿ����� ������ ���̽��θ� ������ �����մϴ�");
        CharacterRenewal();

        //�÷��̾� Ŀ���� ������Ƽ
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "GameReady", false } });

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
     
        ChatRPC("<color=yellow>" + "�濡 ���ο� �÷��̾ �����ϼ̽��ϴ�.</color>");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            ChatRPC("<color=red>" + "���� ���� ������ ���� �����ϼ���. �ΰ��ӿ����� ������ ���̽��θ� ������ �����մϴ�");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
        CharacterRenewal();
    }

    void RoomRenewal()
    { 
      RoomInfo.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
    //�泪�����ư 
    public void OnClickOutRoomBtn()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
            roomUI.SetActive(false);
            lobbyUI.SetActive(true);
    }

    //ä�ñ��
    public void OnClickSendChatBtn()
    {
        if(!ChatInput.text.Equals(""))
            pv.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }


    [PunRPC] // RPC�� �÷��̾ �����ִ� �� ��� �ο����� ����ȭ������
    void ChatRPC(string msg)
    {
        Debug.Log("chat PRC ȣ��");
        bool  isInput = false;
        for (int i = 0; i < ChatLog.Length; i++)
            if (ChatLog[i].text == "") //���� ���°��ƴϸ�
            {
                isInput = true;
                ChatLog[i].text = msg;
                break;
            }
        if (!isInput) // ������ ��ĭ�� ���� �ø�
        {
            for (int i = 1; i < ChatLog.Length; i++) ChatLog[i - 1].text = ChatLog[i].text;
            ChatLog[ChatLog.Length - 1].text = msg;
        }
    }

    //ĳ���� ����
    void CharacterRenewal()
    {
        if (PhotonNetwork.LocalPlayer.NickName.Equals("Latifa"))
        {
            TaichiRole.GetComponent<Animator>().SetBool("Select", false);
            LatifaRole.GetComponent<Animator>().SetBool("Select", true);
        }
        else if (PhotonNetwork.LocalPlayer.NickName.Equals("Taichi"))
        {
            LatifaRole.GetComponent<Animator>().SetBool("Select", false);
            TaichiRole.GetComponent<Animator>().SetBool("Select", true);
        }
        else
        {
            LatifaRole.GetComponent<Animator>().SetBool("Select", false);
            TaichiRole.GetComponent<Animator>().SetBool("Select", false);
        }
       pv.RPC("clickAble", RpcTarget.All);

    }

    public void OnClickLatifaBtn()
    {
        PhotonNetwork.LocalPlayer.NickName = "Latifa"; 
        ChatRPC("<color=yellow>" + PhotonNetwork.LocalPlayer.NickName + "�� �����ϼ̽��ϴ�</color>");
        CharacterRenewal();
    }

    public void OnClickTaichiBtn()
    {
        PhotonNetwork.LocalPlayer.NickName = "Taichi";
        ChatRPC("<color=yellow>" + PhotonNetwork.LocalPlayer.NickName + "�� �����ϼ̽��ϴ�</color>");
        CharacterRenewal();

    }

    public void OnClickNanBtn()
    {
        PhotonNetwork.LocalPlayer.NickName = "���þ���";
        ChatRPC("<color=yellow>ĳ���ͼ����� ����߽��ϴ�</color>");
        CharacterRenewal();
    }

    //���� ����
    [PunRPC]
    private void clickAble()
    {
        Debug.Log("clickAble RPC ȣ��");
        bool startAble = true;
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            LafitaSelectBtn.interactable = true;
            TaichiSelectBtn.interactable = true;
            startAble = false;
        }
        else
        {
            foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (player.NickName.Equals("���þ���")) startAble = false;
            }
            if (PhotonNetwork.PlayerListOthers[0].NickName.Equals("Latifa"))
            {
                LafitaSelectBtn.interactable = false;
                TaichiSelectBtn.interactable = true;
            }
            if (PhotonNetwork.PlayerListOthers[0].NickName.Equals("Taichi"))
            {
                LafitaSelectBtn.interactable = true;
                TaichiSelectBtn.interactable = false;
            }
            if (PhotonNetwork.PlayerListOthers[0].NickName.Equals("���þ���"))
            {
                LafitaSelectBtn.interactable = true;
                TaichiSelectBtn.interactable = true;
            }
        }
        StartBtn.interactable = startAble;
    }
    public void OnClickGameStart()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["GameReady"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        pv.RPC("ChatRPC", RpcTarget.Others, "<color=yellow>" + PhotonNetwork.LocalPlayer.NickName + "�� �غ�Ϸ��Դϴ�.</color>");
        bool gameStart = true;
        foreach (int id in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            if (!(bool)PhotonNetwork.CurrentRoom.Players[id].CustomProperties["GameReady"]) gameStart = false;
        }
        if(gameStart)
        {
            Debug.Log("���ӽ�ŸƮ");
            pv.RPC("ChatRPC", RpcTarget.All, "<color=red>3�� �� ������ ���۵˴ϴ�.</color>");
            pv.RPC("GameStart", RpcTarget.All);
        }
    }

    [PunRPC]
    private void GameStart()
    {
        Invoke("GameScene", 3f);
    }

    private void GameScene()
    {
        Debug.Log("���� ������ �̵�");
        SceneManager.LoadScene("testSceneKWJ");
    }
}
