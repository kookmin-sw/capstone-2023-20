using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class MonsterController : MonoBehaviour
{

    public enum CurrentState { idle, trace, attack, patrol, dead };
    public CurrentState curState = CurrentState.idle;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    // 추적 사정거리
    public float traceDist = 15.0f;
    // 공격 사정거리
    public float attackDist = 3.2f;

    // 사망 여부
    private bool isDead = false;
    private bool isIdle = true;
    private float IdleTIme = 3f;
    private float chkTime = 0f;


    [SerializeField] Transform[] m_ptPoints = null; // 정찰 위치들을 담을 배열
    int m_ptPointsCnt = 0;

    //view
    MonsterView view;

    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
        view = GameObject.Find("Monster").GetComponent<MonsterView>();


        // 추적 대상의 위치를 설정하면 바로 추적 시작
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, traceDist);
        switch (curState)
        {
            case CurrentState.trace:
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, 1f);
                break;
            case CurrentState.patrol:
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, 3f);
                break;
        }
    }
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.5f);

            //float dist = Vector3.Distance(playerTransform.position, _transform.position);


            if (view.isCollision == true)
            {
                Debug.Log("trace");
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.patrol;
            }

        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    _animator.SetBool("isWalk", false);
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    _animator.SetBool("isWalk", true);
                    break;
                case CurrentState.patrol:

                    Patroling();
                    break;
            }

            yield return null;
        }
    }
    void Patroling()
    {
        if (nvAgent.remainingDistance < 2f)
        {
            _animator.SetBool("isWalk", false);
            chkTime += Time.deltaTime;
        }
        if (chkTime > IdleTIme)
        {
            Debug.Log(chkTime);
            chkTime = 0;
            _animator.SetBool("isWalk", true);
            if (nvAgent.remainingDistance < 2f)
            {
                nvAgent.SetDestination(m_ptPoints[m_ptPointsCnt].position);
                Debug.Log(m_ptPointsCnt);
                m_ptPointsCnt++;

                if (m_ptPointsCnt >= m_ptPoints.Length) //포인트를 끝까지 돌면 다시 0으로 초기화
                    m_ptPointsCnt = 0;
                if (!nvAgent.pathPending)
                {
                    if (nvAgent.remainingDistance <= nvAgent.stoppingDistance)
                    {
                        if (!nvAgent.hasPath || nvAgent.velocity.sqrMagnitude == 0f)
                        {
                            curState = CurrentState.idle;
                        }
                    }
                }
            }
        }
    }

}