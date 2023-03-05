using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    // Edit -> Project Settings -> Script Execution Order
    // Add EventManager -100 �� ���� Awake�� �ٸ� ��ü�麸�� ���� ȣ����� �����Ѵ�.
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