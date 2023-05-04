using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] levers;
    void Start()
    {
        //levers = GameObject.Find("Shield Metall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverDoorTagChange()
    {
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject lever in cups)
        {
            lever.tag = "EventObj";
        }
    }
}
