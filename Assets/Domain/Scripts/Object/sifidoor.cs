using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;

public class sifidoor : MonoBehaviour
{
    public UnityEvent Event;

    Animator animator;
    AudioSource audiosource;
    public AudioClip DoorOpen;
    public AudioClip DoorLocked;

    [SerializeField]
    private bool LockState = false;

    int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        audiosource.volume = 0.5f;
        audiosource.playOnAwake = false;
        DoorOpen = audiosource.clip;
    }


    // Update is called once per frame
    void Update()
    {
        //if ( // 열쇠 획득코드 ))
        //{
        //    LockState = true;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Taichi" || other.tag == "Latifa")
        {
            animator.SetBool("IsOpen", true);
            audiosource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Taichi" || other.tag == "Latifa") && animator.GetBool("IsOpen") == true)
        {
            animator.SetBool("IsOpen", false);
            audiosource.Play();
        }
    }

    // 다른 오브젝트와 상호작용용 함수
}
