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
    private TMP_Text tip;
    [SerializeField]
    private TMP_Text explain;
    [SerializeField]
    private GameObject[] BGI;


    static int nextLevel;
    //타이틀, 본관, 체육관, 연구실, 게임오버씬, 로딩씬 순으로 빌드
    static string[] levels = { "MainTitle","PhotonTest-KKB" ,"MainBuilding", "Stage2", "Stage3","" };
    static string[] tips = { 
        "2인으로 플레이가 가능합니다.", 
        "현재와 미래의 학교는 연결되어 있는 건가..?", 
        "왜 이렇게 체육관에 공이 널부러져 있지?..", 
        "연구실 팁은 뭘 줘야될까...", 
         };
    static string[] explains = { 
        "타이틀로 이동 중...", 
        "본관으로 들어가는 중..", 
        "체육관으로 들어가는 중..", 
        "의문의 연구실로 들어가는 중.."
    };

    public static void LoadScene()
    {
        PhotonNetwork.LoadLevel("LoadingScene");

    }

    void Start()
    {
        nextLevel = (int)PhotonNetwork.CurrentRoom.CustomProperties["CurrentLevel"];
        Debug.Log("nextlevel == " + nextLevel);
        tip.text = "Tips : " + tips[nextLevel];
        explain.text = explains[nextLevel];
        BGI[nextLevel - 1].SetActive(true);
        
        if (PhotonNetwork.IsMasterClient)
        {
            LoadStage();
        }
    }

    void LoadStage()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {

        PhotonNetwork.LoadLevel(levels[nextLevel]);
        while (PhotonNetwork.LevelLoadingProgress < 1f)
        {
            progressBar.fillAmount = PhotonNetwork.LevelLoadingProgress;
            yield return null;
        }
        BGI[nextLevel - 1].SetActive(false);
    }
}
