using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;
using Photon.Pun;

public class LaptopController : MonoBehaviour
{
    public UnityEvent Event;

    public Animator animator;
    public TimeAttack timer;
    AudioSource audiosource;

    int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer.gameActive == true)
        {
            animator.SetBool("pass", true);
        }

    }
    public void Activate()
    {


        if (animator.GetBool("pass") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            animator.SetBool("pass", true);
        }
        //else if (animator.GetBool("IsOpen") == true && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        //{
        //    animator.SetBool("IsOpen", false);
        //    audiosource.Play();
        //}

    }


}
