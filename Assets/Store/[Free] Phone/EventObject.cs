using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    public GameObject EventUI;
    

    public GameObject getText()
    {
        return Text;
    }

    public GameObject getEventUI()
    {
        return EventUI;
    }
}
