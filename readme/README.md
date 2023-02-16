# 작업 내용 설명 -김기범 


### 멀티플레이 임시 환경 

```c#
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
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            //룸에 있는 유저이름이랑 고유변호 확인
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}");
        }
        //TODO : 방에있는 캐릭터가 아스나 인지 총안쏘는애(임시)에 따라 인스턴트화할 프리팹 바꿔줘야됨
        //방에들어오면 인스턴트화할 resourcse 안의 prefabs의 string, 벡터값, 회전값 넣어서 복사
        PhotonNetwork.Instantiate("Asuna", Vector3.zero, Quaternion.identity);

    }
}
```

### 플레이어 카메라 설정

플레이어가 Room에 인스턴트화되면 시네머신카메라가 그 플레이어를 찾아서 비추도록 기능을 추가했음

**ThirdAsunaController.cs**
```c#
  //포톤뷰 객체
    private PhotonView pv;
    //보통상태 시네머신 카메라
    private CinemachineVirtualCamera virtualCamera;
    //에임상태일때 시네머신카메라
    private CinemachineVirtualCamera aimVirtualCamera;
    //카메라 root
    private GameObject cameraRoot;
```
필드에 위 4개의 객체를 선언.

```c#
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        CinemachineVirtualCamera[] cameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            if (camera.Name == "AsunaFollowCamera") virtualCamera = camera;
            else if (camera.Name == "AsunaAimCamera") aimVirtualCamera = camera;
        }
        cameraRoot = GameObject.FindGameObjectWithTag("CinemachineTarget");
        // 자신의 로컬 캐릭터의 경우 시네머신 카메라 연결
        if (pv.IsMine)
        {
            virtualCamera.Follow = cameraRoot.transform;
            aimVirtualCamera.Follow = cameraRoot.transform;
        }
                                .
                                .
                                .
    }
```
Asuna(마탄총을 사용하는 캐릭터명 임시)의 경우 에임상태의 카메라와 일반상태의 카메라 2종이 있기 때문에  
FindObjectsOfType함수로 시네머신 카메라타입의 오브젝트를 cameras 배열에 담아주고  
각 카메라의 이름으로 매칭시켜서 카메라객체에 담아준다음 포톤뷰 객체가 자신의 로컬일 경우  
Asuna의 cameraRoot를 각 카메라의 객체의 Follow에 담아줌 이걸로 Room에 입장시 시네머신 카메라가 자동으로  
자신의 로컬 캐릭터를 따라다니게됨

### 플레이어 움직임, 애니메이션 동기화
Player 캐릭터에 다음의 script를 컴포넌트로 추가
+ Photon View : Player의 행동을 관찰한 후 동기화 시키는 컴포넌트 현재까지는 Observer하는 컴포넌트는 transform, animation
+ Photon Transform View : Player의 Transform(Position, Rotation, Scale) 값을 동기화
+ Photon Animation View : Player의 애니메이션을 동기화

![동기화 화면](https://user-images.githubusercontent.com/28584160/213841472-59494766-596c-414b-8e9a-0b016ef0e094.png)

빌드해서 확인해본 결과 카메라 동작, 움직임, 애니메이션까지는 동기화가 잘된다..  
하지만 캐릭터가 총을 쏘게되면, 단발로 쏠 경우 동기화가 돼서 다른 화면에 보이는데 연발로 쏘면 동기화가 잘안된다  
내 로컬 성능 문제인지. 코드 문제인지는 잘 모르겠다.. 

TODO : 
Room에 입장 할때, 이미 마탄총 쓰는 요원(Asuna)이 있을 경우 마탄총을 안쓰는 플레이어로 인스턴트화 한다던지의 별도 작업(회의 내용에 따라), 연발 총 발사 동기화, 캐릭터 현재 체력 등의 캐릭터 별로 가지는 변수값이 구현되면 변수값 동기화 등

   

