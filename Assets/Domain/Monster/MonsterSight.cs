using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  // OnDrawGizmos
using static UnityEditor.PlayerSettings;

public class MonsterSight : MonoBehaviour
{
    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    public bool isCollision = false;

    //��ֹ� �Ǵ�
    [SerializeField] bool isObstacle = false;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Update()
    {


        //
        Vector3 interV = target.position - transform.position;

        //
        //Debug.DrawRay(transform.position, transform.forward * radius, Color.green);
        //float dstToTarget = Vector3.Distance(transform.position, target.position);
        //if (Physics.Raycast(transform.position, interV.normalized, dstToTarget, targetMask))
        //{
        //    isObstacle = false;
        //    Debug.Log("noObs");
        //}
        //else
        //{
        //    Debug.Log("obstacle");
        //    isObstacle = true;
        //}





        if (isObstacle == false)
        { 
            // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
            if (interV.magnitude <= radius)
            {
                // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
                float dot = Vector3.Dot(interV.normalized, transform.forward);
                // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
                float theta = Mathf.Acos(dot);
                // angleRange�� ���ϱ� ���� degree�� ��ȯ
                float degree = Mathf.Rad2Deg * theta;

                // �þ߰� �Ǻ�
                if (degree <= angleRange / 2f)
                    isCollision = true;
                else
                    isCollision = false;

            }
            else
                isCollision = false;
        }
    }

    // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}