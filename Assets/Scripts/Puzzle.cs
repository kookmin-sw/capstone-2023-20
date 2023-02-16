using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Puzzle : MonoBehaviour
{
    public bool state;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        this.state = false;
    }

    // Update is called once per frame
    public void Activate()
    {
        if (state == false)
        {
            target.SetActive(true);
            print("생겨나");
            this.state = true;
            
        }
        else
        {
            target.SetActive(false);
            print("사라져");
            this.state = false;
        }

    }

}
