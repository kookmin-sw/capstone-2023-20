//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;  // OnDrawGizmos
//using static UnityEditor.PlayerSettings;

//public class MonsterView : MonoBehaviour
//{
//    public Transform target;    // 부채꼴에 포함되는지 판별할 타겟
//    public float angleRange = 30f;
//    public float radius = 3f;

//    Color _blue = new Color(0f, 0f, 1f, 0.2f);
//    Color _red = new Color(1f, 0f, 0f, 0.2f);

//    public bool isCollision = false;

//    //장애물 판단
//    [SerializeField] bool isObstacle = false;
//    [SerializeField] LayerMask targetMask;
//    [SerializeField] LayerMask obstacleMask;

//    [HideInInspector]
//    public List<Transform> visibleTargets = new List<Transform>();

//    void Update()
//    {


//        //
//        Vector3 interV = target.position - transform.position;

//        //
//        //Debug.DrawRay(transform.position, transform.forward * radius, Color.green);
//        //float dstToTarget = Vector3.Distance(transform.position, target.position);
//        //if (Physics.Raycast(transform.position, interV.normalized, dstToTarget, targetMask))
//        //{
//        //    isObstacle = false;
//        //    Debug.Log("noObs");
//        //}
//        //else
//        //{
//        //    Debug.Log("obstacle");
//        //    isObstacle = true;
//        //}





//        if (isObstacle == false)
//        { 
//            // target과 나 사이의 거리가 radius 보다 작다면
//            if (interV.magnitude <= radius)
//            {
//                // '타겟-나 벡터'와 '내 정면 벡터'를 내적
//                float dot = Vector3.Dot(interV.normalized, transform.forward);
//                // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
//                float theta = Mathf.Acos(dot);
//                // angleRange와 비교하기 위해 degree로 변환
//                float degree = Mathf.Rad2Deg * theta;

//                // 시야각 판별
//                if (degree <= angleRange / 2f)
//                    isCollision = true;
//                else
//                    isCollision = false;

//            }
//            else
//                isCollision = false;
//        }
//    }

//    // 유니티 에디터에 부채꼴을 그려줄 메소드
//    private void OnDrawGizmos()
//    {
//        Handles.color = isCollision ? _red : _blue;
//        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
//        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
//        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
//    }
//}