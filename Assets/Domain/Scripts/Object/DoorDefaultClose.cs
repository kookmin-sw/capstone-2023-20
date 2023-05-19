using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class DoorDefaultClose : MonoBehaviour
{
    public UnityEvent Event;

    public Animator animator;
    AudioSource audiosource;
    public AudioClip DoorOpen;
    public AudioClip DoorLocked;

    public PhotonView pv;

    [SerializeField]
    private bool LockState = false;
    [SerializeField]
    private DoorDefaultClose ConnectedDoor;
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
    public void Activate()
    {

        if (LockState == false)
        {
            audiosource.clip = DoorOpen;
            //성현 - 상호작용을 연속으로 입력할 시 문이 동작하기 전에 state가 다시 변경하는 것을 방지
            if (animator.GetBool("IsOpen") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
            {
                Debug.Log("Default");
                animator.SetBool("IsOpen", true);
                audiosource.Play();
            }
            else if (animator.GetBool("IsOpen") == true && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            {
                Debug.Log("Open");
                animator.SetBool("IsOpen", false);
                audiosource.Play();
            }
        }
        else if (LockState == true)
        {
            audiosource.clip = DoorLocked;
            audiosource.Play();

        }

    }



    // 다른 오브젝트와 상호작용용 함수
    public void InteractActivate(bool OncePlay)
    {
        // 잠김상태가 해제되었을 때만
        if (LockState == false)
        {
            if (OncePlay)
            {
                if (count == 0)
                {
                    Event.Invoke();
                    count++;
                }

            }
            else
            {
                Event.Invoke();
            }
        }
    }

    public void UnLockDoor()
    {
        if (LockState == true)
        {
            LockState = false;
            Debug.Log("UnLock Other too");
            UnLockOther();
            pv.RPC("UnLockOther", RpcTarget.Others, "broadcastrooom1");
            audiosource.clip = DoorLocked;
            audiosource.Play();
        }
        

    }

    public void UnLockOther()
    {
        LockState = false;
    }
    
    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        animator.SetBool("IsOpen", true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        animator.SetBool("IsOpen", false);
    //    }
    //}
}
