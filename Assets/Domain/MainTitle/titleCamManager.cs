using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleCamManager : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

    private Animator cam1Anim;
    private Animator cam2Anim;
    private Animator cam3Anim;

    public CanvasGroup canvasgroup;
    public bool fadein;
    public bool fadeout;
    public float TimeToFade = 1.5f;
    float time = 0;

    void Start()
    {

        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
        cam1Anim = cam1.GetComponent<Animator>();
        cam2Anim = cam2.GetComponent<Animator>();
        cam3Anim = cam3.GetComponent<Animator>();

        InvokeRepeating("CamSwitch", 5f, 0.5f); // a초 뒤 시작하고, b초마다 반복.
    }

    // Update is called once per frame
    void Update()
    {

        if (fadein)
        {
            Debug.Log(canvasgroup.alpha);
            if (canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += Time.deltaTime / 0.8f;
                if (canvasgroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout)
        {
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= Time.deltaTime / 0.8f;
                if (canvasgroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }
    void CamSwitch()
    {

        if (cam1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f && cam1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f) //1 종료
        {
            Debug.Log("cam1end");
            fadein = true;
            cam2.SetActive(false);
            cam3.SetActive(true);
        }
        else if (cam1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            fadeout = true;
            cam1.SetActive(false);
            cam2.SetActive(true);
        }

        else if (cam2Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f && cam2Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f) 
        {
            fadein = true;
        }
        else if (cam2Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            fadeout = true;
            cam2.SetActive(false);
            cam3.SetActive(true);
        }
        else if (cam3Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f && cam3Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f) 
        {
            fadein = true;
        }
        else if (cam3Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            fadeout = true;
            cam3.SetActive(false);
            cam1.SetActive(true);
        }
        return;
    }


}
