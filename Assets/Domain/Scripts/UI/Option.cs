using System.Collections;
using System.Collections.Generic;
using Photon.Voice;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider voice;
    [SerializeField]
    private Slider bgm; 
    [SerializeField]
    private Slider sfx;

    private GameObject manager;
    private const float sensitivityMin = 10.0f;
    private const float sensitivityMax = 100.0f;

    private void Awake()
    {
        manager = GameObject.Find("NetworkManager");   
    }
    public void SetSoundVoice()
    {
        mixer.SetFloat("Voice", voice.value);
        int perc = (int)voice.value + 80;
        voice.GetComponentInChildren<TMP_Text>().text = perc.ToString() + '%';
    }
    public void SetSoundBGM()
    {
        mixer.SetFloat("BGM", bgm.value);
        int perc = (int)bgm.value + 80;
        bgm.GetComponentInChildren<TMP_Text>().text = perc.ToString()+'%';
    }
    public void SetSoundSFX()
    {
        mixer.SetFloat("SFX", sfx.value);
        int perc = (int)sfx.value + 80;
        sfx.GetComponentInChildren<TMP_Text>().text = perc.ToString()+'%';
    }

    public void OnClickOut()
    {
        warning.SetActive(true);
        
    }

    public void QuickOption()
    {
        gameObject.SetActive(false);
    }

    public void SetSensitivity()
    {
       
    }

    //warning
    public void OnRealOutBtn()
    {
        manager.GetComponent<NetworkManager>().LeaveRoom();
    }

    public void OnBackBtn()
    {
        warning.SetActive(false);
    }
}
