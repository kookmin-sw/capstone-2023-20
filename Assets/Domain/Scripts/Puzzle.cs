using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public bool state;
    public GameObject target;
    private ThirdPlayerController ThirdPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        this.state = false;
        ThirdPlayerController = FindObjectOfType<ThirdPlayerController>();
    }

    // Update is called once per frame
    public void Activate()
    {
        if (state == false)
        {
            target.SetActive(true);
            this.state = true;

        }
        else
        {
            target.SetActive(false);
            this.state = false;
        }

    }

}
