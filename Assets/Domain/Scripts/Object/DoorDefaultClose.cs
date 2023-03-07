using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;

public class DoorDefaultClose : MonoBehaviour
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
        //성현 - 상호작용을 연속으로 입력할 시 문이 동작하기 전에 state가 다시 변경하는 것을 방지
        Debug.Log("Open");
        if (animator.GetBool("IsOpen") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            animator.SetBool("IsOpen", true);
        }
        else if (animator.GetBool("IsOpen") == true && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            animator.SetBool("IsOpen", false);


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
