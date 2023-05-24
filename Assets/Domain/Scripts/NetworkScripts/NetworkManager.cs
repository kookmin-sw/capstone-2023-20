using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class NetworkManager : MonoBehaviourPunCallbacks { 

    public  PhotonView pv;
    [SerializeField]
    private GameObject CloseGameMsg;

    
    private void Awake()
    {
        var objs = FindObjectsOfType<NetworkManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    //게임오버씬으로 이동하는 퍼블릭함수(죽거나, 시간초과나면 이 함수 호출)
    public void GameOver()
    {
        pv.RPC("MoveGameOverScene", RpcTarget.MasterClient);
    }
    [PunRPC]
    private void MoveGameOverScene()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        if (cp.ContainsKey("GameReady")) cp.Remove("GameReady");
        cp.Add("GameReady", false);
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        if (GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName) == null) return;
        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //마우스 커서 되돌림.
        if (PhotonNetwork.IsMasterClient)
        {
            cp = PhotonNetwork.CurrentRoom.CustomProperties;
            if (cp.ContainsKey("InGame")) cp.Remove("InGame"); //충돌 방지 확실하게 삭제후 업데이트 하기 위함;
            if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
            cp.Add("InGame", false);
            cp.Add("GameOver", true); //게임오버상태인지 아닌지
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
        }
        Debug.Log("로컬플레이어 이름 ::" + GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name);
        FadeOut();
    }
    public void OnLevelWasLoaded(int level)
    {
        // 메인빌딩이거나, 게임오버 상태면서, 게임오버씬이 아닐 경우만 플레이어캐릭터 생성
        if(level == 2 || ((bool)PhotonNetwork.CurrentRoom.CustomProperties["GameOver"] && level != 6) ) 
        {
            pv.RPC("CreatePlayer", RpcTarget.All);
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
            cp.Add("GameOver", false);
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
            
        }
        else if(level <= 1 )
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        else if(level == 3 || level == 4) //level == 연구소
        {
            pv.RPC("SetPlayerPos", RpcTarget.All);
        }
    }
    [PunRPC]
    private void CreatePlayer()
    {
        GameObject chk = GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName);
        if (chk != null) return;
        Transform pos = GameObject.Find("SpwanPoint" + PhotonNetwork.LocalPlayer.NickName).transform;
        
        PhotonNetwork.Instantiate("Player" + PhotonNetwork.LocalPlayer.NickName, pos.position, pos.rotation);

        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>().virtualCamera.Priority += 10;
        Debug.Log(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name + " 생성완료 크레이트플레이어");
        
    }
    [PunRPC]
    private void SetPlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName);
        if (player != null)
        {
            Debug.LogError("플레이어 캐릭터가 없어요!!!");
            return;
        }
        Transform pos = GameObject.Find("SpwanPoint" + PhotonNetwork.LocalPlayer.NickName).transform;

        player.transform.position = pos.position;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CloseGameMsg.SetActive(true);
        Invoke("LeaveRoom",3f);
    }
    
    public void LeaveRoom()
    {
        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //마우스 커서 되돌림.
        Debug.Log(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name + " 파괴하고 방 나가기");
        PhotonNetwork.Destroy(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName));
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainTitle");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("cause : " + cause.ToString());
    }

    public override void OnLeftRoom()
    {
        Debug.Log("방을 나갔습니다.");
    }
    private void FadeOut()
    {
        if(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>() != null)
        {
            GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>().FadingStart();
        }
        PhotonNetwork.Destroy(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName));
        GameOverManager.LoadGameOver();
    }
















    //씬을 로드할때 사용되는 함수 ==> 다음 씬으로 가기위한 조건이 충족되면 아래 함수를 호출해주면 됩니다 복붙해서 사용해주세요
    /*  
    void func(){
            if(!PhotonNetwork.isMasterClient) return;
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
