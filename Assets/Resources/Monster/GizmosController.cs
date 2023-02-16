using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void OnDrawGizoms() //씬에서 항상 보임
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one); //정사각형 그리기
    }

    void OnDrawGizmosSelected() //오브젝트가 선택됐을 때 보임
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 3f); //구 그리기
    }
}
