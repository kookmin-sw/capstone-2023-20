using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CCTVScreenTransition : MonoBehaviour
{
    public Texture[] CCTVArray = new Texture[9];
    private Texture CurrentCCTV;
    public Transform CurrentScreen;
    private int CurrentIndex = 0;
    private float Times = 0;
    
    void Start()
    {
        CurrentCCTV = CCTVArray[CurrentIndex];
        CurrentScreen = gameObject.transform.Find("CurrentScreen");
        CurrentScreen.GetComponent<RawImage>().texture = CurrentCCTV;
    }

    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            Times += Time.deltaTime;
            CurrentScreen.gameObject.transform.Find("Timer").GetComponent<TextMeshProUGUI>().text = Times.ToString("F2");
        }
    }

    public void transRight()
    {
        CurrentIndex++;
        if (CurrentIndex >= 9)
        {
            CurrentIndex = 0;
        }
        CurrentCCTV = CCTVArray[CurrentIndex];
        CurrentScreen.GetComponent<RawImage>().texture = CurrentCCTV;
        
        CurrentScreen.gameObject.transform.Find("CameraNum").GetComponent<TextMeshProUGUI>().text = string.Format("Camera {0}", CurrentIndex + 1);
    }

    public void transLeft()
    {
        CurrentIndex--;
        if (CurrentIndex < 0)
        {
            CurrentIndex = 8;
        }
        CurrentCCTV = CCTVArray[CurrentIndex];
        CurrentScreen.GetComponent<RawImage>().texture = CurrentCCTV;
       
        CurrentScreen.gameObject.transform.Find("CameraNum").GetComponent<TextMeshProUGUI>().text = string.Format("Camera {0}", CurrentIndex + 1);
    }

    public void closeView()
    {
        this.gameObject.SetActive(false);
    }
    
}
