using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTransition : MonoBehaviour
{
    [SerializeField] private GameObject TransMap;

    public GameObject getMap()
    {
        return TransMap;
    }
}
