using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 0.5f;
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

    }
}
