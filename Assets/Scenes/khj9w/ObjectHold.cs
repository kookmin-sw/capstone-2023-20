using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public GameObject Object;
    public Transform PlayerTransform;
    public float range = 3f;
    public float Go = 100f;
    public Camera Camera;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey("r"))
        {
            Debug.Log("r");
            StartPickUp();
        }

        if (Input.GetKeyUp("r"))
        {
            Drop();
        }
    }

    void StartPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            HoldTarget target = hit.transform.GetComponent<HoldTarget>();
            if (target != null)
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
//Gun.GetComponent<Rigidbody>().isKinematic = true;
        Object.transform.SetParent(PlayerTransform);
    }

    void Drop()
    {
        PlayerTransform.DetachChildren();
        //Gun.GetComponent<Rigidbody>().isKinematic = false;
    }

}