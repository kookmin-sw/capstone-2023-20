using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVoiceSource(float volume)
    {
        mixer.SetFloat("Voice",volume);
    }
}
