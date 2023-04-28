using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    Animator animator;
    private Locker Locker;

    void Start()
    {
        animator = GetComponent<Animator>();
        Locker = gameObject.transform.parent.GetComponent<Locker>();
    }

    public void Activate()
    {
        if (Locker.unLock == true)
        {
            if (animator.GetBool("IsOpen") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
            {
                animator.SetBool("IsOpen", true);
                Debug.Log("?");
            }
            else if (animator.GetBool("IsOpen") == true && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
                animator.SetBool("IsOpen", false);

        }
    }
}
