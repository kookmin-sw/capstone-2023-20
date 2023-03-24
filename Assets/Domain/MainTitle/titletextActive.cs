using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titletextActive : MonoBehaviour
{
    public GameObject cam1;
    public GameObject text;

    private Animator cam1Anim;


    void Start()
    {

        cam1.SetActive(true);
        text.SetActive(false);
        cam1Anim = cam1.GetComponent<Animator>();

        InvokeRepeating("textChk", 10f, 5f); // a�� �� �����ϰ�, b�ʸ��� �ݺ�.
    }

    void textChk()
    {

        if (cam1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) //1 ����
        {
            text.SetActive(true);
        }

        return;
    }


}
