using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; 

//KB - PhotonManger class
public class PhotonManager : MonoBehaviourPunCallbacks //콜백함수 override를 위해 상속
{
    //버전
    private readonly string version = "1.0f";
    // 사용자 아이디 입력
    private string userId = "Tester";

    private void Awake()
    {   
        //임시 빌드화면
        Screen.SetResolution(960, 540, false);
        //같은 룸의 유저들에게 자동으로 씬을 로딩
        PhotonNetwork.AutomaticallySyncScene = true;
        //같은 버전의 유저끼리 접속하도록 만듦
        PhotonNetwork.GameVersion = version;
        //유저 id 초기화
        PhotonNetwork.NickName = userId;
        //photon server와 통신 횟수 설정, 초당 30
        Debug.Log(PhotonNetwork.SendRate);

        //server in
        PhotonNetwork.ConnectUsingSettings();

    }

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // ==> false
        //로비 입장
        PhotonNetwork.JoinLobby(); 
    }

    //로비 입장시 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // == > true

        PhotonNetwork.JoinRandomRoom(); // 랜덤 매치 기능을 하는 함수
        //TODO : 방에있는 캐릭터 종류와 새로 방에 들어오는 캐릭터가 다르도록 구현

    }

    //랜덤한 룸 입장이 실패했을 경우 호출되는 콜백 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}:{message}");

        //룸 속성 정의
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; //최대 접속자 수
        roomOptions.IsOpen = true; //룸 오픈여부
        roomOptions.IsVisible = true; //로비에서 룸 목록에 보이게할지안할지

        //룸 생성
        PhotonNetwork.CreateRoom("Test Room", roomOptions);
    }
    // 룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Test Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }
    //룸에 입장 후 호출되는 콜백 함수

    public override void OnJoinedRoom()
    {
        
        Debug.Log($"PhotonNetwork.InRoom = { PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        //룸에 접속한 사용자 이름
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            //룸에 있는 유저이름이랑 고유변호 확인
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}");
        }
        //TODO : 방에있는 캐릭터가 아스나 인지 총안쏘는애(임시)에 따라 인스턴트화할 프리팹 바꿔줘야됨
        //방에들어오면 인스턴트화할 resourcse 안의 prefabs의 string, 벡터값, 회전값 넣어서 복사
        PhotonNetwork.Instantiate("Player2", new Vector3(-136.33f, 3.63f, 26.319f), Quaternion.identity);
        //GameObject.Find("Plyaer").SetActive(true);

    }

}
