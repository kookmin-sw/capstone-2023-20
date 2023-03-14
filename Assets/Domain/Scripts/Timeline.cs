using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Timeline : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public TimelineAsset timeline;

    private void Start()
    {
        playableDirector.playOnAwake = false;
    }

    public void Play()
    {
        playableDirector.Play();
    }

    public void PlayFromTimeline()
    {
        playableDirector.Play(timeline);
    }

}