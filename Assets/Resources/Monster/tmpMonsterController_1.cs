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

//    public enum CurrentState { idle, trace, attack, patrol, p_dead }; //피격?당함?추가해야하나
//    public CurrentState curState = CurrentState.idle;

//    private Transform monsterTransform;

//    private Vector3 playerPrePos;

//    private NavMeshAgent m_nvAgent;
//    private Animator m_animator;

//     추적 사정거리
//    public float traceDist = 15.0f;
//     공격 사정거리
//    public float attackDist = 3.2f;
//     위험 사정 거리, 이 거리 안에 들어오면 플레이어 사망
//    public float dangerDist = 0.5f;

//     사망 여부
//    private bool p_isDead = false;

//    timeline
//    public bool startMonster = false;
//    public VideoPlayer video;


//    순찰
//    [SerializeField] Transform[] m_ptPoints = null; // 정찰 위치들을 담을 배열
//    private int m_ptPointsCnt = 0;

//    bool doPatrol = false;
//    float idleingTime = 3f;

//    void Start()
//    {
//        monsterTransform = this.gameObject.GetComponent<Transform>();

//        m_nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
//        m_animator = this.gameObject.GetComponent<Animator>();

//         추적 대상의 위치를 설정하면 바로 추적 시작
//         nvAgent.destination = playerTransform.position;
//        StartCoroutine(this.CheckState());
//        StartCoroutine(this.CheckStateForAction());
//    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.white;
//        Gizmos.DrawWireSphere(transform.position, traceDist);
//        switch (curState)
//        {
//            case CurrentState.idle:
//                break;
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

//    IEnumerator CheckState()
//    {
//        while (!p_isDead)
//        {
//            yield return new WaitForSeconds(0.5f);
//            playerPrePos = GameObject.FindWithTag("Player").GetComponent<PlayerPreviousTransform>().ptransform;

            
//            float playerMonsterDist = Vector3.Distance(playerPrePos, monsterTransform.position); // 플레이어와 몬스터 거리

//            if (playerMonsterDist <= attackDist)
//            {
//                curState = CurrentState.attack;
//            }
//            else if (playerMonsterDist <= traceDist) // 플레이어가 탐지 범위 안에 들어가면 추격
//            {
//                curState = CurrentState.trace;


//                if (!startMonster) //몬스터 첫등장 비디오
//                {
//                    video.gameObject.SetActive(true);
//                    video.Play();
//                    video.loopPointReached += CheckOver;
//                }
//                else
//                {
//                    curState = CurrentState.trace;
//                }
//                Debug.Log(playerPrePos);
//            }
//            else //순찰
//            {
//                curState = CurrentState.patrol;
//                idleingTime -= Time.deltaTime;
//                Debug.Log("idle");
//                if (idleingTime <= 0)
//                {
//                    idleingTime = 3f;
//                    Debug.Log("patrol");
//                    curState = CurrentState.patrol;
//                }
//            }
//            else
//            {
//                curState = CurrentState.idle;
//            }
//        }
//    }

   
//    void CheckOver(UnityEngine.Video.VideoPlayer vp)
//    {
//        Debug.Log("video end");
//        video.gameObject.SetActive(false);
//        curState = CurrentState.trace;
//        startMonster = true;
//    }

//    IEnumerator CheckStateForAction()
//    {
//        while (!p_isDead)
//        {
//            switch (curState)
//            {
//                case CurrentState.idle:
//                    m_animator.SetBool("isWalk", false);
//                    m_animator.SetBool("isRun", false);
//                    break;
//                case CurrentState.trace:
//                    Debug.Log("trace");
//                    m_nvAgent.destination = playerPrePos;
//                    Traceing();
//                    m_animator.SetBool("isWalk", false);
//                    m_animator.SetBool("isRun", true);
//                    break;
//                case CurrentState.patrol:
//                    StartCoroutine("Idleing");
//                    Patroling();
//                    m_animator.SetBool("isWalk", true);
//                    break;
//                case CurrentState.attack:
//                    m_animator.SetBool("isAttack", true); // attack 되면 플레이어 즉사???
//                    break;
//            }

//            yield return null; // 매프레임마다 실행
//        }
//    }
//    IEnumerable Idleing()
//    {
//        float delay = 3f;
//        m_animator.SetBool("isWalk", false);
//        m_animator.SetBool("isRun", false);
//        while (delay > 0f)
//        {
//            Debug.Log("Idle " + delay);
//            yield return null;
//            delay -= Time.deltaTime;
//        }
//    }
//    void Patroling()
//    {
//        Debug.Log("point cnt " + m_ptPointsCnt);
//        if (m_nvAgent.velocity == Vector3.zero) // ai의 속도가 0이 되면 다음 포인트로 이동
//        {
//            m_nvAgent.SetDestination(m_ptPoints[m_ptPointsCnt++].position);

//            if (m_ptPointsCnt >= m_ptPoints.Length) //포인트를 끝까지 돌면 다시 0으로 초기화
//                m_ptPointsCnt = 0;
//        }
//    }
//    void Traceing()
//    {
//    }
//    void Attacking() { }
//}