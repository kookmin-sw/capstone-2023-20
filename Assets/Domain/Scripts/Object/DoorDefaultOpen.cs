using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;

public class DoorDefaultOpen : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        audiosource.volume = 0.5f;
        audiosource.playOnAwake = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        //���� - ��ȣ�ۿ��� �������� �Է��� �� ���� �����ϱ� ���� state�� �ٽ� �����ϴ� ���� ����
        Debug.Log("Open");
        if (animator.GetBool("IsOpen") == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            animator.SetBool("IsOpen", false);
            audiosource.Play();
        }
        else if (animator.GetBool("IsOpen") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
        {
            animator.SetBool("IsOpen", true);
            audiosource.Play();
        }


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
