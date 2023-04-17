using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class colliderActive : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Taichi" || other.tag == "Latifa")
        {
            gameObject.GetComponent<ObjectManager>().Activate();

        }
    }
}
