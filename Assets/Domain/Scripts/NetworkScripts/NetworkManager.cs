using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class NetworkManager : MonoBehaviourPunCallbacks { 

    public  PhotonView pv;
    [SerializeField]
    private GameObject CloseGameMsg;

    [Header("FadeOut")]
    private float fadeSpeed = 2f;
    private Image fadeImage;
    private float delayTime = 1.0f;  // 전환 대기 시간




    

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

        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //마우스 커서 되돌림.
        Debug.Log("로컬플레이어 이름 ::" + GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name);
        fadeImage = GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponentInChildren<FadeOut>().GetComponent<Image>();
        StartCoroutine(FadeOut());
        if(PhotonNetwork.IsMasterClient) GameOverManager.LoadGameOver();
    }
    public void OnLevelWasLoaded(int level)
    {
        if(level == 1 || (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameOver"] ) 
        {
            if (!PhotonNetwork.IsMasterClient) return;
            pv.RPC("CreatePlayer", RpcTarget.All);
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
            cp.Add("GameOver", false);
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
            
        }
        else if(level == 0)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
    [PunRPC]
    private void CreatePlayer()
    {
        Transform pos = GameObject.Find("SpwanPoint" + PhotonNetwork.LocalPlayer.NickName).transform;
        PhotonNetwork.Instantiate("Player" + PhotonNetwork.LocalPlayer.NickName, pos.position, pos.rotation);

        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>().virtualCamera.Priority += 10;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName));
        Debug.Log(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name + " 생성완료 크레이트플레이어");
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
        /*Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
        if (cp.ContainsKey("CurrentLevel")) cp.Remove("CurrentLevel"); //충돌 방지 확실하게 삭제후 업데이트 하기 위함;
        cp.Add("CurrentLevel", 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp);*/
        LoadingSceneController.LoadScene();
        PhotonNetwork.LeaveRoom();
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

    IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        // 패널의 알파 값을 서서히 감소시켜 페이드아웃 효과를 줌
        while (fadeImage.color.a < 1.0f)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                                           fadeImage.color.a + fadeSpeed * Time.deltaTime);
            yield return null;
        }

        // 전환 대기 시간 동안 대기
        yield return new WaitForSeconds(delayTime);
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
