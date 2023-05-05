using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public int number;
    public bool state = false;
    public DoorDefaultClose[] DDC;
    public LeversController LeverController;

    // Start is called before the first frame update
    void Start()
    {
        DDC = GetComponentsInChildren<DoorDefaultClose>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwichOff()
    {
        DDC[1].animator.SetBool("IsOpen", false);
    }

    public void SwichOff1s()
    {
        Invoke("SwichOff", 1f);
    }



    public void leverOn()
    {
        if (DDC[0].animator.GetBool("IsOpen"))
        {
            state = true;
            LeverController.NumberCheck(number);
        }
        else
        {
            state = false;
        }
    }
}
