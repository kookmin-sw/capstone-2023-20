using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAnswer : MonoBehaviour
{
    public Animation animation; // 애니메이션 컴포넌트를 참조할 변수
    public AudioSource audioSource; // 사운드를 재생할 오디오 소스 컴포넌트


    private void Start()
    {
        animation = GetComponent<Animation>(); // 애니메이션 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }
    void EnableAnswer()
    {
        animation.enabled = true; // 애니메이션 컴포넌트 활성화
        audioSource.enabled = true;
    }
}
