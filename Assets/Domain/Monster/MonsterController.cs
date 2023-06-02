using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class MonsterController : MonoBehaviour
{
    [SerializeField] string playerTag = "Taichi";
    public enum CurrentState { idle, trace, attack, patrol, dead };
    public CurrentState curState = CurrentState.idle;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;
    public int pt;



    // 사망 여부 + 추가필요 플레이어 상태 등
    private bool isDead = false;
    //
    private bool flagIdle = true;
    [SerializeField]
    private float IdleTime = 5f;
    private float chkTime = 0f;
    [SerializeField]
    private float traceSpeed = 3f;
    [SerializeField]
    private float patrolSpeed = 2f;

    //몬스터 시야
    public AiSensor Sensor;
    bool inSight = false;
    bool isLost = false;

    [SerializeField] Transform[] m_ptPoints = null; // 정찰 위치들을 담을 배열

    private AudioSource audioSource;

    private GameObject wayPoint;
    //

    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag(playerTag).GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
        Sensor = GameObject.Find("MonsterSensor").GetComponent<AiSensor>();
        wayPoint = GameObject.Find("WayPoints");

        m_ptPoints = wayPoint.gameObject.GetComponentsInChildren<Transform>();
        audioSource = GetComponent<AudioSource>();

        // 추적 대상의 위치를 설정하면 바로 추적 시작
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
        StartCoroutine(RecalculatePathRoutine());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 10f); // 소리들리기 시작하는 거리

    }
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.5f);

            //float dist = Vector3.Distance(playerTransform.position, _transform.position);
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > 10f)
            {
                // 거리가 10f보다 멀어지면 오디오 소스의 볼륨을 0으로 설정
                audioSource.volume = 0f;
            }
            else
            {
                // 거리가 maxDistance 이내일 경우 오디오 소스의 3D 세팅된 볼륨 크기로 설정
                float volume = 1f - (distanceToPlayer / 10.0f); // 거리에 따라 볼륨 조절
                audioSource.volume = volume;
            }
            if (distanceToPlayer < 0.5f)
            {
                PlayerDeath();
            }
            inSight = Sensor.isInSight;
            rayChkDoor(); // 문열기
            if (inSight)
            {
                isLost = true;
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
                case CurrentState.trace: // 추적 상태
                    nvAgent.speed = traceSpeed;
                    Vector3 targetPosition = playerTransform.position - (transform.position - playerTransform.position).normalized * 2.0f;
                    nvAgent.destination = targetPosition;
                    _animator.SetBool("isRun", true);
                    break;
                case CurrentState.patrol:
                    if (isLost) // 플레이어 찾았는데 놓친 경우
                    {
                        isLost = false;
                        _animator.SetBool("isIdle", true);
                        nvAgent.ResetPath(); // 경로 초기화
                        yield return new WaitForSeconds(IdleTime); // 일정 시간 동안 대기
                    }
                    if (CheckIdleState())
                    {
                        _animator.SetBool("isWalk", true);
                        int pt = Random.Range(0, m_ptPoints.Length);
                        Debug.Log("pt: " + pt);
                        nvAgent.SetDestination(m_ptPoints[pt].position);
                    }
                    nvAgent.speed = patrolSpeed; // 순찰 최대 이동 속도
                    Patroling();
                    break;
            }

            yield return null;
        }
    }
    IEnumerator RecalculatePathRoutine()
    {
        while (!isDead)
        {
            if (curState == CurrentState.trace) // 추격 상태에서만 경로를 재계산합니다
            {
                // 네비게이션 경로를 재계산합니다
                nvAgent.SetDestination(playerTransform.position);
            }
            //5초에 한번
            yield return new WaitForSeconds(5.0f);
        }
    }
    void Patroling()
    {
        if (nvAgent.remainingDistance < 1f && chkTime < IdleTime && flagIdle) // 일정시간동안 idle
        {
            chkTime += Time.deltaTime;
            //nvAgent.ResetPath();
        }
        if (chkTime > IdleTime) // idle 종료, 순찰 이동
        {
            flagIdle = false;
            Debug.Log("chkTime : " + chkTime);
            chkTime = 0;

            if (nvAgent.remainingDistance < 1f)
            {
                _animator.SetBool("isWalk", true);

                //순찰포인트 랜덤 순찰
                pt = Random.Range(0, m_ptPoints.Length);
                Debug.Log("pt >"+ pt);

                nvAgent.SetDestination(m_ptPoints[pt].position);
                //여기서 멈추네?
                if (!nvAgent.pathPending)
                {
                    Debug.Log("pp");
                    if (nvAgent.remainingDistance <= nvAgent.stoppingDistance)
                    {
                        Debug.Log("rm");
                        if (!nvAgent.hasPath || nvAgent.velocity.sqrMagnitude == 0f) // 도착
                        {
                            curState = CurrentState.patrol;
                            _animator.SetBool("isIdle", true);
                            flagIdle = true;
                        }
                    }
                }
            }
        }
    }
    bool CheckIdleState()
    {
        Debug.Log(nvAgent.velocity.magnitude);
        if (nvAgent.velocity.magnitude < 0.9f)
        {
            chkTime += Time.deltaTime;
            if (chkTime > 3f)
            {
                chkTime = 0f;
                Debug.Log("chkIdle");
                return true;
            }
        }
        else
        {
            chkTime = 0f;
        }
        return false;
    }

    void rayChkDoor()
    {
        RaycastHit hit;
        float MaxDistance = 2f;// 레이의 길이
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.CompareTag("EventObj") && hit.distance < 2.0f)
            {
                Debug.DrawRay(_transform.position, _transform.forward * MaxDistance, Color.blue, 3.0f);
                GameObject.Find(hit.collider.name).GetComponent<ObjectManager>().Activate();
            }

        }
    }
    void PlayerDeath()
    {
        //플레이어 사망
    }
}
