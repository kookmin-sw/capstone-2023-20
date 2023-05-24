using UnityEngine;
using UnityEngine.Video;
using Photon.Pun;
using Photon.Realtime;
public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string NextSceneName;

    private void Start()
    {
        // 비디오 플레이어 컴포넌트 가져오기
        videoPlayer = GetComponent<VideoPlayer>();

        // Loop Point Reached 이벤트에 이벤트 핸들러 등록
        videoPlayer.loopPointReached += OnLoopPointReached;
    }

    private void OnLoopPointReached(VideoPlayer source)
    {
        // 비디오 재생이 끝나면 다음 씬으로 전환
        if (PhotonNetwork.IsMasterClient) LoadingSceneController.LoadScene();
    }
}
