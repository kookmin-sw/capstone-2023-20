//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.Playables;
//using UnityEngine.Video;

//public class tmpMonsterController : MonoBehaviour
//{

//    public enum CurrentState { patrol, trace, attack, p_dead }; //피격?당함?추가해야하나
//    public CurrentState curState = CurrentState.patrol;

//    private Transform monsterTransform;

//    private Vector3 playerPrePos;

//    private NavMeshAgent m_nvAgent;
//    private Animator m_animator;

//    // 추적 사정거리
//    public float traceDist = 15.0f;
//    // 공격 사정거리
//    public float attackDist = 3.2f;
//    // 위험 사정 거리, 이 거리 안에 들어오면 플레이어 사망
//    public float dangerDist = 0.5f;

//    // 사망 여부
//    private bool p_isDead = false;

//    //timeline
//    //public bool startMonster = false;
//    //public VideoPlayer video;


//    //순찰
//    [SerializeField] Transform[] m_ptPoints = null; // 정찰 위치들을 담을 배열
//    private int m_ptPointsCnt = 0;

//    float idleingTime = 3f;
//    bool isTrace = false;
//    bool isPatrolIdle = false;

//    void Start()
//    {
//        monsterTransform = this.gameObject.GetComponent<Transform>();

//        m_nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
//        m_animator = this.gameObject.GetComponent<Animator>();

//        // 추적 대상의 위치를 설정하면 바로 추적 시작
//        // nvAgent.destination = playerTransform.position;
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.white;
//        Gizmos.DrawWireSphere(transform.position, traceDist);
//        switch (curState)
//        {
//            case CurrentState.trace:
//                Gizmos.color = Color.red;
//                Gizmos.DrawWireSphere(transform.position, 1f);
//                break;
//            case CurrentState.patrol:
//                Gizmos.color = Color.green;
//                Gizmos.DrawWireSphere(transform.position, 3f);
//                break;
//        }
//    }
//    int ChkDist()
//    {
//        playerPrePos = GameObject.FindWithTag("Player").GetComponent<PlayerPreviousTransform>().ptransform;

//        //
//        float playerMonsterDist = Vector3.Distance(playerPrePos, monsterTransform.position); // 플레이어와 몬스터 거리

//        if (playerMonsterDist <= attackDist)
//        {
//            curState = CurrentState.attack;
//            return 2;
//        }
//        else if (playerMonsterDist <= traceDist) // 플레이어가 탐지 범위 안에 들어가면 추격
//        {
//            curState = CurrentState.trace;
//            return 1;

//        }
//        else 
//        {
//            curState = CurrentState.patrol;
//            return 0;
//        }; //순찰
//    }
//    enum state { patrol, attack, trace };
//    void Update()
//    {
//        while (!p_isDead)
//        {
//            int state = ChkDist();
//        }
//    }

   
//    //void CheckOver(UnityEngine.Video.VideoPlayer vp)
//    //{
//    //    Debug.Log("video end");
//    //    video.gameObject.SetActive(false);
//    //    curState = CurrentState.trace;
//    //    startMonster = true;
//    //}

//    IEnumerator CheckStateForAction()
//    {
//        Debug.Log("chk action");
//        while (!p_isDead)
//        {
//            switch (curState)
//            {
//                case CurrentState.trace:
//                    Debug.Log("trace");
//                    m_nvAgent.destination = playerPrePos;
//                    m_animator.SetBool("isWalk", false);
//                    m_animator.SetBool("isRun", true);
//                    break;
//                case CurrentState.patrol:
//                    m_animator.SetBool("isWalk", false);
//                    m_animator.SetBool("isRun", false);
//                    while (ChkDist() == 0)
//                    {
//                        m_animator.SetBool("isWalk", false);
//                        m_animator.SetBool("isRun", false);
//                        float delay = 3f;
//                        while (delay > 0f)
//                        {
//                            yield return null;
//                            delay -= Time.deltaTime;
//                        }
//                        Debug.Log("Idle end");
//                        m_animator.SetBool("isWalk", true);
//                        m_nvAgent.SetDestination(m_ptPoints[m_ptPointsCnt++].position);
//                        bool isArrived = false;
//                        while (!isArrived)
//                        {
//                            if (!m_nvAgent.pathPending)
//                            {
//                                if (m_nvAgent.remainingDistance <= m_nvAgent.stoppingDistance)
//                                {
//                                    if (!m_nvAgent.hasPath || m_nvAgent.velocity.sqrMagnitude == 0f)
//                                    {
//                                        isArrived = true;
//                                        m_ptPointsCnt++;
//                                        if (m_ptPointsCnt >= m_ptPoints.Length) //포인트를 끝까지 돌면 다시 0으로 초기화
//                                            m_ptPointsCnt = 0;
//                                    }
//                                }
//                            }
//                        }
//                        Debug.Log("end patrol");
//                    }
//                    break;
//                case CurrentState.attack:
//                    //m_animator.SetBool("isAttack", true); // attack 되면 플레이어 즉사???
//                    break;
//            }

//            yield return null; // 매프레임마다 실행
//        }
//    }


//}