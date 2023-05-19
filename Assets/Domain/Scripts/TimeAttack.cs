using UnityEngine;
using TMPro;
using System.Collections;
using Photon.Pun;


public class TimeAttack : MonoBehaviour
{

    private float done = 10.0F;

    public TMP_Text gui_text;
    public ObjectManager objectManager;

    // 전체 제한 시간을 설정해준다.
    public float time;
    float setTime;

    int min;
    float sec;

    // 타이머 시작
    public bool gameActive;

    // Use this for initialization

    void Start()
    {
        gameActive = false;
        objectManager = GetComponent<ObjectManager>();

    }


    // Update is called once per frame

    void Update()
    {
        if (gameActive)
        {
            // 남은 시간을 감소시켜준다.
            setTime -= Time.deltaTime;

            // 전체 시간이 60초 보다 클 때
            if (setTime >= 60f)
            {
                // 60으로 나눠서 생기는 몫을 분단위로 변경
                min = (int)setTime / 60;
                // 60으로 나눠서 생기는 나머지를 초단위로 설정
                sec = setTime % 60;
                // UI를 표현해준다
                gui_text.text = "남은 시간 : " + min + "분" + (int)sec + "초";
            }

            // 전체시간이 60초 미만일 때
            if (setTime < 60f)
            {
                // 분 단위는 필요없어지므로 초단위만 남도록 설정
                gui_text.text = "남은 시간 : " + (int)setTime + "초";
            }

            // 남은 시간이 0보다 작아질 때
            if (setTime <= 0)
            {
                // UI 텍스트를 0초로 고정시킴.
                gui_text.text = "남은 시간 : 0초";
                objectManager.Activate();
                gameActive = false;

            }
        }
    }

    [PunRPC]
    public void StartGame()
    {
        gameActive = true;
        setTime = time;
    }
}
