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

    // ���� �����Ÿ�
    public float traceDist = 7.0f;
    // ���� �����Ÿ�
    //public float attackDist = 3.2f;

    // ��� ���� + �߰��ʿ� �÷��̾� ���� ��
    private bool isDead = false;
    //
    private bool isIdle = true;
    [SerializeField]
    private float IdleTIme = 7f;
    private float chkTime = 0f;
    [SerializeField]
    private float traceSpeed = 3f;
    [SerializeField]
    private float patrolSpeed = 2f;

    //���� �þ�
    public AiSensor Sensor;
    bool inSight = false;
    bool isLost = false;

    [SerializeField] Transform[] m_ptPoints = null; // ���� ��ġ���� ���� �迭
    int m_ptPointsCnt = 0;

    //view
    //MonsterView view;

    //debug
    //public GameObject alarm;

    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
        //view = GameObject.Find("Monster").GetComponent<MonsterView>();
        Sensor = GameObject.Find("MonsterSensor").GetComponent<AiSensor>();

        // ���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(transform.position, traceDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.5f);

            //float dist = Vector3.Distance(playerTransform.position, _transform.position);
            inSight = Sensor.isInSight;

            if (inSight)
            {
                isLost= true;
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
                //case CurrentState.idle:
                //    _animator.SetBool("isIdle", true);
                //    break;
                case CurrentState.trace:
                    nvAgent.speed = traceSpeed;
                    nvAgent.destination = playerTransform.position;
                    _animator.SetBool("isRun", true);
                    _animator.SetBool("isIdle", false);
                    _animator.SetBool("isWalk", false);
                    break;
                case CurrentState.patrol:
                    if(isLost)
                    {
                        isLost= false;
                        nvAgent.ResetPath();
                    }
                    nvAgent.speed = patrolSpeed; // �ִ� �̵� �ӵ�
                    Patroling();
                    break;
            }

            yield return null;
        }
    }
    void Patroling()
    {
        Debug.Log("remainingDist > "+nvAgent.remainingDistance);
        //idle
        if (nvAgent.remainingDistance < 1f && chkTime < IdleTIme)
        {
            //alarm.SetActive(true);
            //Debug.Log("patrol_idle");
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);
            chkTime += Time.deltaTime;
            //nvAgent.ResetPath();
        }
        if (chkTime > IdleTIme)
        {
            //alarm.SetActive(false);
            Debug.Log("chkTime : " + chkTime);
            chkTime = 0;
            if (nvAgent.remainingDistance < 1f)
            {
                Debug.Log("pointCnt : "+ m_ptPointsCnt);
                _animator.SetBool("isWalk", true);
                _animator.SetBool("isIdle", false);

                Vector3 RandomPos;
                RandomPoint(out RandomPos);
                Debug.DrawRay(RandomPos, Vector3.up, Color.green, 3.0f);

                nvAgent.SetDestination(RandomPos);
                //nvAgent.SetDestination(m_ptPoints[m_ptPointsCnt].position);
                //m_ptPointsCnt++;

                //if (m_ptPointsCnt >= m_ptPoints.Length) //����Ʈ�� ������ ���� �ٽ� 0���� �ʱ�ȭ
                //    m_ptPointsCnt = 0;//�ֱ׷�
                //if (!nvAgent.pathPending)
                //{
                //    if (nvAgent.remainingDistance <= nvAgent.stoppingDistance)
                //    {
                //        if (!nvAgent.hasPath || nvAgent.velocity.sqrMagnitude == 0f) // ����
                //        {
                //            curState = CurrentState.patrol;
                //            _animator.SetBool("isIdle", true);
                //        }
                //    }
                //}
            }
        }
    }
    bool RandomPoint(out Vector3 randomPosResult)
    {
        float range = 10f;
        Vector3 randomPoint = _transform.position + Random.insideUnitSphere * range; //random point in a sphere 
        float maxDist = 1.0f;
        NavMeshHit hit;
        //samplepos �̿��ؼ� ��ֹ� �ִ� ��ġ���� ����Ʈ ���� ����, bake �� ��ġ��
        //NavMeshPermalink : AI ������Ʈ�� �ɾ�ٴ� �� �ִ� ǥ��. �׺���̼� ��θ� ����� �� �ִ� ǥ���� �ȴ�.
        //navmesh.allareas�� �ش��ϴ� navmesh �� maxDist �ݰ� ������ randomPoint���� ���� ����� ��ġ hit�� ����
        if (NavMesh.SamplePosition(randomPoint, out hit, maxDist, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            randomPosResult = hit.position;
            return true;
        }

        randomPosResult = _transform.position; // ���ڸ�
        return false;
    }
    //void MakeWorldBounds()
    //{
    //    WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
    //    Vector3 min = worldBounds.min.position;
    //    Vector3 max = worldBounds.max.position;

    //    Vector3 randomPosition = new Vector3{
    //Random.Range(min.x, max.x),
    //Random.Range(min.y, max.y),
    //Random.Range(min.z, max.z) // �ٵ� �츮���� ���� �������� ���� �𸣰ڴ�
    //}

}