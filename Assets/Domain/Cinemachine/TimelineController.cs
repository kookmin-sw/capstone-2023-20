using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();
    }
}
