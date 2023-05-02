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



    // 사망 여부 + 추가필요 플레이어 상태 등
    private bool isDead = false;
    //
    private bool flagIdle = true;
    [SerializeField]
    private float IdleTIme = 5f;
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

    //debug
    public GameObject alarm;

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

        // 추적 대상의 위치를 설정하면 바로 추적 시작
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 5f); // 랜덤위치 생기는 최대거리

    }
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.5f);

            //float dist = Vector3.Distance(playerTransform.position, _transform.position);
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
                    nvAgent.destination = playerTransform.position;
                    _animator.SetBool("isRun", true);
                    break;
                case CurrentState.patrol:
                    if (isLost) // 플레이어 찾았는데 놓친 경우
                    {
                        isLost = false;
                        nvAgent.ResetPath(); // 경로 초기화
                    }
                    nvAgent.speed = patrolSpeed; // 순찰 최대 이동 속도
                    Patroling();
                    break;
            }

            yield return null;
        }
    }
    void Patroling()
    {
        if (nvAgent.remainingDistance < 1f && chkTime < IdleTIme && flagIdle) // 일정시간동안 idle
        {
            //Debug.Log("patrolIdle remainingDist > " + nvAgent.remainingDistance);
            //alarm.SetActive(true);
            Debug.Log("patrol_idle");
            chkTime += Time.deltaTime;
            //nvAgent.ResetPath();
        }
        if (chkTime > IdleTIme) // idle 종료, 순찰 이동
        {
            flagIdle = false;
            //alarm.SetActive(false);
            Debug.Log("chkTime : " + chkTime);
            chkTime = 0;

            if (nvAgent.remainingDistance < 1f)
            {
                _animator.SetBool("isWalk", true);

                //랜덤포인트 순찰
                //Vector3 RandomPos;
                //RandomPoint(out RandomPos);
                //Vector3 randomDir = (_transform.position - RandomPos).normalized;
                //float dirMagnitude = (_transform.position - RandomPos).magnitude;
                //Debug.DrawRay(_transform.position, randomDir * dirMagnitude, Color.red, 10.0f);
                // 현재위치부터 랜덤위치까지 레이를 그리고 10.0초 동안 보여줌
                //nvAgent.SetDestination(RandomPos);

                //순찰포인트 순서대로 순찰
                int pt = Random.Range(0, m_ptPoints.Length);
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
    bool RandomPoint(out Vector3 randomPosResult)
    {
        randomPosResult = _transform.position; // 제자리

        float range = 5f;
        Vector3 randomPoint = _transform.position + Random.insideUnitSphere * range; //random point in a sphere 
        float maxDist = 3.0f; // 매쉬가 본 갈 수 있는 장소중에서 랜덤포인트와 가까운 거리 찾을때 쓰는 최대거리
        NavMeshHit hit;
        //samplepos 이용해서 장애물 있는 위치에는 포인트 생성 안함, bake 된 위치에
        //NavMeshPermalink : AI 에이전트가 걸어다닐 수 있는 표면. 네비게이션 경로를 계산할 수 있는 표면이 된다.
        //navmesh.allareas에 해당하는 navmesh 중 maxDist 반경 내에서 randomPoint에서 가장 가까운 위치 hit에 리턴
        if (NavMesh.SamplePosition(randomPoint, out hit, maxDist, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            randomPosResult = hit.position;
            return true;
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
}
