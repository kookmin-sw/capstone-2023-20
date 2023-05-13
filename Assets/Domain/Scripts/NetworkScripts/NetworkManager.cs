using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Cockpit.Forms;
using StarterAssets;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks { 
    public  PhotonView pv;
    private GameObject LocalPlayer;
    [SerializeField]
    private GameObject CloseGameMsg;

 
    
    private void Awake()
    {
        
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }
    }

    //게임오버씬으로 이동하는 퍼블릭함수(죽거나, 시간초과나면 이 함수 호출)
    public void GameOver()
    {
        pv.RPC("MoveGameOverScene", RpcTarget.All);
    }
    [PunRPC]
    private void MoveGameOverScene()
    {
        LocalPlayer.GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //마우스 커서 되돌림.
        PhotonNetwork.Destroy(LocalPlayer);
        if(PhotonNetwork.IsMasterClient) GameOverManager.LoadGameOver();
    }
    public void OnLevelWasLoaded(int level)
    {
        if(level == 1 || (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameOver"] ) 
        {
            CreatePlayer(PhotonNetwork.LocalPlayer);
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
            cp.Add("GameOver", false);
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
            
        }
    }
    private void CreatePlayer(Player player)
    {
        Transform pos = GameObject.Find("SpwanPoint" + player.NickName).transform;
        LocalPlayer = PhotonNetwork.Instantiate("Player" + player.NickName, pos.position, pos.rotation);
        LocalPlayer.GetComponent<ThirdPlayerController>().virtualCamera.Priority += 10;
        DontDestroyOnLoad(LocalPlayer);
        Debug.Log(LocalPlayer.name + " 생성완료 크레이트플레이어");
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CloseGameMsg.SetActive(true);
        Invoke("LeaveRoom",3f);
    }
    
    public void LeaveRoom()
    {
        LocalPlayer.GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //마우스 커서 되돌림.
        Debug.Log(LocalPlayer.name + " 파괴하고 방 나가기");
        PhotonNetwork.LeaveRoom();
        Destroy(LocalPlayer);
        SceneManager.LoadScene(0);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("cause : " + cause.ToString());
    }


    //씬을 로드할때 사용되는 함수 ==> 다음 씬으로 가기위한 조건이 충족되면 아래 함수를 호출해주면 됩니다 복붙해서 사용해주세요
    /*  
    void func(){
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            int nextLevel = (int)cp["CurrentLevel"] + 1;
            if (cp.ContainsKey("CurrentLevel")) cp.Remove("CurrentLevel"); //충돌 방지 확실하게 삭제후 업데이트 하기 위함;
            cp.Add("CurrentLevel", nextLevel);
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("CurrentLevel"))
            {
                LoadingSceneController.LoadScene();
            }
    }
    */
}
