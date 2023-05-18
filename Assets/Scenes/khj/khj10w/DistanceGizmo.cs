using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGizmo : MonoBehaviour
{
    public float distance;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
