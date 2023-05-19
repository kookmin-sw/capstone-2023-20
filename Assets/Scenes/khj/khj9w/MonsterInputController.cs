using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInputController : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);
        transform.position += move.normalized * speed * Time.deltaTime;
    }
}
