using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    // Edit -> Project Settings -> Script Execution Order
    // Add EventManager -100 을 통해 Awake가 다른 객체들보다 먼저 호출됨을 보장한다.
    void Awake()
    {
        Instance = this;
    }

    public event Action<int> onDoorTriggerEnter;

    public void OnDoorTriggerEnter(int id)
    {
        if (onDoorTriggerEnter != null)
        {
            onDoorTriggerEnter(id);
        }
    }

    public event Action<int> onDoorTriggerExit;

    public void OnDoorTriggerExit(int id)
    {
        if (onDoorTriggerExit != null)
        {
            onDoorTriggerExit(id);
        }
    }
}