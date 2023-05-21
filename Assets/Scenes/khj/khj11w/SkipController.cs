using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SkipController : MonoBehaviourPunCallbacks
{
    public string NextSceneName;


    public void OnLevelWasLoaded(int level)
    {
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.Instantiate("SkipBtn",new Vector3(683,384,0),new Quaternion(0f,0f,0f,0f));
    }

    public void SkipButtonClicked()
    {
        // 게임 플레이 스킵 로직 작성
        // 예: 다음 씬으로 전환
        if (PhotonNetwork.IsMasterClient) LoadingSceneController.LoadScene();
        else Debug.Log("방장이 아닙니다. 스킵안됩니다.213ㄹㅇㄴㅁㄹㅇㄴㄻ");
    }
}
