using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private PhotonView pv;
    [SerializeField]
    private TMP_Text tip;
    [SerializeField]
    private TMP_Text explain;
    [SerializeField]
    private GameObject[] BGI;



    private int nextLevel;
    //타이틀, 본관, 체육관, 연구실, 게임오버씬, 로딩씬 순으로 빌드
    //테스트 씬 - PhotonTest-KKB;
    private string[] levels = { "MainTitle", "Mainbuilding", "Gym", "scify_ysh", "" };
    private string[] tips = {
        "2인으로 플레이가 가능합니다.",
        "현재와 미래의 학교는 연결되어 있는 건가..?",
        "왜 이렇게 체육관에 공이 널부러져 있지?..",
        ".......",
         };
    private string[] explains = {
        "타이틀로 이동 중...",
        "본관으로 들어가는 중..",
        "체육관으로 들어가는 중..",
        "의문의 연구실로 들어가는 중.."
    };
    public static void LoadScene()
    {
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("LoadingScene");
    }
    
    void Awake()
    {
        
        nextLevel = (int)PhotonNetwork.CurrentRoom.CustomProperties["CurrentLevel"];
        Debug.Log("nextlevel == " + levels[nextLevel]);
        tip.text = "Tips : " + tips[nextLevel];
        explain.text = explains[nextLevel];
        BGI[nextLevel].SetActive(true);
        if (PhotonNetwork.AutomaticallySyncScene) Debug.Log("씬이 자동 동기화가 됩니다.");
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("PhotonNetwork : Trying to Load a level but we are not the master Client at LSC");
            return;
        }
        else pv.RPC("StartLoadSceneProcess", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void StartLoadSceneProcess()
    {
        if (PhotonNetwork.IsMasterClient) StartCoroutine(LoadSceneProcess());
    }
   
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levels[nextLevel]); // 비동기로 씬 로딩 시작
        asyncLoad.allowSceneActivation = false; // 씬 활성화를 잠시 중지

        while (!asyncLoad.isDone) // 씬 로딩이 완료될 때까지 반복
        {
            progressBar.fillAmount = asyncLoad.progress; // 진행 상황을 프로그래스 바에 반영

            if (asyncLoad.progress >= 0.9f) // 씬 로딩이 거의 완료되었을 때
            {
                asyncLoad.allowSceneActivation = true; // 씬 활성화 허용
            }

            yield return null;
        }

        BGI[nextLevel].SetActive(false); // 로딩이 완료되면 BGI 비활성화
    }

}
