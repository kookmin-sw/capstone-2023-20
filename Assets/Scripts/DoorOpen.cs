using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DoorOpen : Object
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate()
    {
        if (animator.GetBool("IsOpen") == false)
            animator.SetBool("IsOpen", true);
        else
            animator.SetBool("IsOpen", false);
        Thread.Sleep(100);
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
