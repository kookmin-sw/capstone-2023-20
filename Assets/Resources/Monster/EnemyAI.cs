using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent m_enemy = null;

    [SerializeField] Transform[] m_ptPoints = null; // 정찰 위치들을 담을 배열
    int m_ptPointsCnt = 0;

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        InvokeRepeating("MovetoNextWayPoint", 0f, 2f); // 시작하면 2초마다 반복
    }
    void MoveToNextWayPoint()
    {
        if (m_enemy.velocity == Vector3.zero) // ai의 속도가 0이 되면 다음 포인트로 이동
        {
            m_enemy.SetDestination(m_ptPoints[m_ptPointsCnt++].position);

            if (m_ptPointsCnt >= m_ptPoints.Length) //포인트를 끝까지 돌면 다시 0으로 초기화
                m_ptPointsCnt = 0;
        }
    }

    void Update()
    {
        
    }
}
