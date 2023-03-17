using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviousTransform : MonoBehaviour
{
    public Vector3 ptransform;
    void Start()
    {
        //Debug.Log("ppt");
        ptransform = this.gameObject.GetComponent<Transform>().position;
        InvokeRepeating("SavePlayerPreTransform", 0f, 0.5f); // 시작하면 n초마다 반복
    }

    void SavePlayerPreTransform()
    {
        //Debug.Log("save player pre transform");
        ptransform = this.gameObject.GetComponent<Transform>().position;
        //Debug.Log("ppt >>> " + ptransform);
    }
    void Update()
    {
        //Debug.Log(ptransform);
    }
}
