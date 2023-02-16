using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    public GameObject EventUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Text.SetActive(false);
        }
    }

    public GameObject getEventUI()
    {
        return EventUI;
    }
}
