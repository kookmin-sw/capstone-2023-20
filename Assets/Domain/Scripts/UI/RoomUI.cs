using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


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

    private void Awake()
    {
    
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return) && ChatInput.text != "")
        {
            OnClickSendChatBtn();
        }
    }
    public override void OnJoinedRoom()
    {
        //닉네임 설정 리팩토링(2주차)
        if (PhotonNetwork.MasterClient.NickName.Equals("Taichi")) PhotonNetwork.LocalPlayer.NickName = "Latifa";
        else PhotonNetwork.LocalPlayer.NickName = "Taichi";

        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame"]) //방 상태가 인게임 상태면 바로 게임시작
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "GameReady", true } });
            GameScene();
        }
        else //방 
        {
            lobbyUI.SetActive(false);
            roomUI.SetActive(true);
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "GameReady", false } });
            RoomRenewal();
            ChatInput.text = "";
            for (int i = 0; i < ChatLog.Length; i++) ChatLog[i].text = "";
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2) ChatRPC("<color=red>" + "서로 무슨 역할을 할지 조율하세요. 인게임에서는 오로지 보이스로만 소통이 가능합니다");
            CharacterRenewal();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + "방에 새로운 플레이어가 참가하셨습니다.</color>");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            ChatRPC("<color=red>" + "서로 무슨 역할을 할지 조율하세요. 인게임에서는 오로지 보이스로만 소통이 가능합니다");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
        CharacterRenewal();
    }

    void RoomRenewal()
    { 
      RoomInfo.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
    //방나가기버튼 
    public void OnClickOutRoomBtn()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
            roomUI.SetActive(false);
            lobbyUI.SetActive(true);
    }

    //채팅기능
    public void OnClickSendChatBtn()
    {
        if(!ChatInput.text.Equals(""))
            pv.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }


    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 동기화시켜줌
    void ChatRPC(string msg)
    {
        Debug.Log("chat PRC 호출");
        bool  isInput = false;
        for (int i = 0; i < ChatLog.Length; i++)
            if (ChatLog[i].text == "") //꽉찬 상태가아니면
            {
                isInput = true;
                ChatLog[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatLog.Length; i++) ChatLog[i - 1].text = ChatLog[i].text;
            ChatLog[ChatLog.Length - 1].text = msg;
        }
    }

    //캐릭터 선택
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
        ChatRPC("<color=yellow>" + PhotonNetwork.LocalPlayer.NickName + "을 선택하셨습니다</color>");
        CharacterRenewal();
    }

    public void OnClickTaichiBtn()
    {
        PhotonNetwork.LocalPlayer.NickName = "Taichi";
        ChatRPC("<color=yellow>" + PhotonNetwork.LocalPlayer.NickName + "을 선택하셨습니다</color>");
        CharacterRenewal();

    }

    public void OnClickNanBtn()
    {
        PhotonNetwork.LocalPlayer.NickName = "선택안함";
        ChatRPC("<color=yellow>캐릭터선택을 취소했습니다</color>");
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["GameReady"] = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        CharacterRenewal();
    }

    //게임 시작
    [PunRPC]
    private void clickAble()
    {
        Debug.Log("clickAble RPC 호출");
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
                if (player.NickName.Equals("선택안함")) startAble = false;
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
            if (PhotonNetwork.PlayerListOthers[0].NickName.Equals("선택안함"))
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
        pv.RPC("ChatRPC", RpcTarget.Others, "<color=yellow>" + PhotonNetwork.LocalPlayer.NickName + "는 준비완료입니다.</color>");
        bool gameStart = true;
        foreach (int id in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            if (!(bool)PhotonNetwork.CurrentRoom.Players[id].CustomProperties["GameReady"]) gameStart = false;
        }
        if(gameStart)
        {
            Debug.Log("게임스타트");
            pv.RPC("ChatRPC", RpcTarget.All, "<color=red>3초 후 게임이 시작됩니다.</color>");
            Hashtable rp = PhotonNetwork.CurrentRoom.CustomProperties;
            rp["InGame"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(rp);
            pv.RPC("GameStart", RpcTarget.All);
            StartCoroutine(Count());
        }
    }
    IEnumerator Count()
    {
        pv.RPC("ChatRPC", RpcTarget.All, "<color=red>3</color>");
        yield return new WaitForSeconds(1.0f);
        pv.RPC("ChatRPC", RpcTarget.All, "<color=red>2</color>");
        yield return new WaitForSeconds(1.0f);
        pv.RPC("ChatRPC", RpcTarget.All, "<color=red>1</color>");
        yield return new WaitForSeconds(1.0f);
    }
    [PunRPC]
    private void GameStart()
    {
 
        Invoke("GameScene", 3f);
    }

    private void GameScene()
    {

        Debug.Log("게임 씬으로 이동");
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
        if (cp.ContainsKey("CurrentLevel")) cp.Remove("CurrentLevel"); //충돌 방지 확실하게 삭제후 업데이트 하기 위함;
        if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
        cp.Add("CurrentLevel", 1);
        cp.Add("GameOver", false); //게임오버상태인지 아닌지
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("PhotonNetwork : Trying to Load a level but we are not the master Client at RoomUI");
            return;
        }
        else LoadingSceneController.LoadScene();
        
    }
}

