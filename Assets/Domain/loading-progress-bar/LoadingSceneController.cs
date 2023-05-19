using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class LoadingSceneController : MonoBehaviour
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
    //테스트 씬 - PhotonTest_KKB;
    private string[] levels = { "MainTitle", "MainBuilding", "Stage2", "Stage3", "" };
    private string[] tips = {
        "2인으로 플레이가 가능합니다.",
        "현재와 미래의 학교는 연결되어 있는 건가..?",
        "왜 이렇게 체육관에 공이 널부러져 있지?..",
        "연구실 팁은 뭘 줘야될까...",
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
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("PhotonNetwork : Trying to Load a level but we are not the master Client at LSC");
            return;
        }
        else pv.RPC("StartLoadSceneProcess", RpcTarget.All);
    }

    [PunRPC]
    private void StartLoadSceneProcess()
    {
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess()
    {

        if(PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel(levels[nextLevel]);
        while (PhotonNetwork.LevelLoadingProgress < 1f)
        {
            progressBar.fillAmount = PhotonNetwork.LevelLoadingProgress;
            yield return null;
        }
        BGI[nextLevel].SetActive(false);
    }
}
