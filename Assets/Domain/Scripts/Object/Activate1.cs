using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Activate1 : MonoBehaviour
{
    public bool state;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("exit");
            if (state)
                Deactivate();
        }
    }

    // Update is called once per frame
    [PunRPC]
    public void Activate()
    {
        if (state == false)
        {
            target.SetActive(true);
            print("생겨나");
            state = true;
        }
        else
        {
            target.SetActive(false);
            print("사라져");
            state = false;
        }
    }

    public void Deactivate()
    {
        target.SetActive(false);
        print("사라져");
        state = false;
    }
}
