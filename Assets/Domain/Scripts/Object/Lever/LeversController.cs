using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] levers;
    public int OrderNumber = 0;
    private int ClearNumber;
    void Start()
    {
        //levers = GameObject.Find("Shield Metall");
        ClearNumber = levers.Length - 1;
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
    public void NumberCheck(int num)
    {
        if (num != OrderNumber)
        {
            //게임 초기화
            Debug.Log("순서 틀림");
            OrderNumber = 0;
            foreach (GameObject lever in levers)
            {
                lever.GetComponent<lever>().SwichOff1s();
            }

        }
        else if (num == ClearNumber)
        {
            Debug.Log("clear");
        }

        else
        {
            OrderNumber++;
        }
    }
}
