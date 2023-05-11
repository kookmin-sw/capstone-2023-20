using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Cockpit.Forms;
using StarterAssets;

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
            pv.RPC("MoveGameOverScene", RpcTarget.All);
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
        LocalPlayer.GetComponent<StarterAssetsInputs>().PlayerMoveLock();
        GameOverManager.LoadGameOver((int)PhotonNetwork.CurrentRoom.CustomProperties["CurrentLevel"]);
    }
 

    public void OnLevelWasLoaded(int level)
    {
        if(level ==1 ) 
        {
            CreatePlayer(PhotonNetwork.LocalPlayer);
            
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
        Debug.Log("cause" + cause.ToString());
    }


}
