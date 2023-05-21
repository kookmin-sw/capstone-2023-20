using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipController : MonoBehaviour
{
    public string NextSceneName;

    private void Awake()
    {
      
    }
    public void SkipButtonClicked()
    {
        // 게임 플레이 스킵 로직 작성
        // 예: 다음 씬으로 전환
        if (PhotonNetwork.IsMasterClient) LoadingSceneController.LoadScene();
    }
}
