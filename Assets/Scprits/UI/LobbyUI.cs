using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class LobbyUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject titleUI;
    [SerializeField] 
    private GameObject lobbyUI;
    [SerializeField]
    private TMP_Text statusText;
    [SerializeField]
    private TMP_InputField RoomName;
    [SerializeField]
    private Button[] Rooms;
    [SerializeField]
    private Button PrevBtn, NextBtn;
    int currentPage = 1, maxPage, multiple;
    List<RoomInfo> myList = new List<RoomInfo>();
    private void Update()
    {
        statusText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
    }
    public void OnclickCreateRoomBtn()
    {
        //방이름이 있으면
        if (RoomName.text != "")
        {
            CreateRoom();
        }
        else //방이름이 없으면 애니메이션 실행
        {
            RoomName.GetComponent<Animator>().SetTrigger("On");
        }
    }
  
    // < 버튼 -2 , > 버튼 -1 , 방 숫자
    public void OnClickRoomList(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        RoomListRenewal();
    }
    public void OnClickBackBtn()
    {
        RoomName.text = "";
        PhotonNetwork.Disconnect();
        Debug.Log("온라인 환경 나감");
        lobbyUI.SetActive(false);
        titleUI.SetActive(true);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        RoomListRenewal();
    }


    void RoomListRenewal()
    {
        // 최대페이지
        maxPage = (myList.Count % Rooms.Length == 0) ? myList.Count / Rooms.Length : myList.Count / Rooms.Length + 1;

        // 이전, 다음버튼
        PrevBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * Rooms.Length;
        for (int i = 0; i < Rooms.Length; i++)
        {
            Rooms[i].interactable = (multiple + i < myList.Count) ? true : false;
            Rooms[i].transform.GetChild(0).GetComponent<TMP_Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            Rooms[i].transform.GetChild(1).GetComponent<TMP_Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedLobby()
    {
        myList.Clear();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(RoomName.text + "생성 완료");
        PhotonNetwork.LocalPlayer.NickName = "Taichi";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = message;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 입장 실패");
        statusText.text = message;
    }
}

