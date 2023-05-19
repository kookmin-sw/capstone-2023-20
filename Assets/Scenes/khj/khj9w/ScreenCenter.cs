using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCenter : MonoBehaviour
{

    private Vector3 center;

    // Use this for initialization
    void Start()
    {
        center = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);

    }
}