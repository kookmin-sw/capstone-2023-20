using UnityEngine;

public class FogInGymController : MonoBehaviour
{
    private int fogLevel = 1;
    private float timePassed = 0f;
    private float fogIncreaseInterval = 5f;
    private float playerDeathDelay = 20f; // 3단계가 지난 후 추가로 5초가 지나면 플레이어 사망 함수를 호출

    public Color fogColor = Color.magenta; // 안개의 보라색

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Taichi"))
        {
            RenderSettings.fog = true; // 안개를 활성화합니다.
            float density = 0.1f;
            RenderSettings.fogDensity = density;
            RenderSettings.fogColor = fogColor; // 안개의 색상을 설정합니다.
            InvokeRepeating("IncreaseFogLevel", fogIncreaseInterval, fogIncreaseInterval);
            Invoke("PlayerDeath", playerDeathDelay);
        }
    }

    private void IncreaseFogLevel()
    {
        fogLevel++;
        Debug.Log("안개 단계 " + fogLevel + " 증가");

        // 안개 밀도 조절
        float density = fogLevel * 0.2f; // 예시로 밀도를 fogLevel에 비례하게 증가시킵니다.
        RenderSettings.fogDensity = density;

        if (fogLevel >= 3)
        {
            CancelInvoke("IncreaseFogLevel");
        }
    }

    private void PlayerDeath()
    {
        // 플레이어 사망 시 호출되는 함수입니다.
        // 원하는 동작을 작성해주세요.
        // 예를 들어, 게임 오버 화면을 표시하거나 다른 게임 로직을 실행할 수 있습니다.
    }
}
