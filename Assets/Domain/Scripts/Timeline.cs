using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Timeline : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public TimelineAsset timeline;
    public bool isPlay = false;

    private void Start()
    {
        playableDirector = this.playableDirector;
        playableDirector.playOnAwake = false;
    }

    public void PlayMustOne()
    {
        // 성현 - 한번만 재생하기 위함.
        if (isPlay == false)
        {
            isPlay = true;
            playableDirector.Play();
        }

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