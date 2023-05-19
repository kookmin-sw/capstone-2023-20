using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipController : MonoBehaviour
{
    public string NextSceneName;
    public void SkipButtonClicked()
    {
        // 게임 플레이 스킵 로직 작성
        // 예: 다음 씬으로 전환
        SceneManager.LoadScene(NextSceneName);
    }
}
