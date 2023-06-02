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



    // ��� ���� + �߰��ʿ� �÷��̾� ���� ��
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

    //���� �þ�
    public AiSensor Sensor;
    bool inSight = false;
    bool isLost = false;

    [SerializeField] Transform[] m_ptPoints = null; // ���� ��ġ���� ���� �迭

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

        // ���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
        StartCoroutine(RecalculatePathRoutine());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 10f); // �Ҹ��鸮�� �����ϴ� �Ÿ�

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
                // �Ÿ��� 10f���� �־����� ����� �ҽ��� ������ 0���� ����
                audioSource.volume = 0f;
            }
            else
            {
                // �Ÿ��� maxDistance �̳��� ��� ����� �ҽ��� 3D ���õ� ���� ũ��� ����
                float volume = 1f - (distanceToPlayer / 10.0f); // �Ÿ��� ���� ���� ����
                audioSource.volume = volume;
            }
            if (distanceToPlayer < 0.5f)
            {
                PlayerDeath();
            }
            inSight = Sensor.isInSight;
            rayChkDoor(); // ������
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
                case CurrentState.trace: // ���� ����
                    nvAgent.speed = traceSpeed;
                    Vector3 targetPosition = playerTransform.position - (transform.position - playerTransform.position).normalized * 2.0f;
                    nvAgent.destination = targetPosition;
                    _animator.SetBool("isRun", true);
                    break;
                case CurrentState.patrol:
                    if (isLost) // �÷��̾� ã�Ҵµ� ��ģ ���
                    {
                        isLost = false;
                        _animator.SetBool("isIdle", true);
                        nvAgent.ResetPath(); // ��� �ʱ�ȭ
                        yield return new WaitForSeconds(IdleTime); // ���� �ð� ���� ���
                    }
                    if (CheckIdleState())
                    {
                        _animator.SetBool("isWalk", true);
                        int pt = Random.Range(0, m_ptPoints.Length);
                        Debug.Log("pt: " + pt);
                        nvAgent.SetDestination(m_ptPoints[pt].position);
                    }
                    nvAgent.speed = patrolSpeed; // ���� �ִ� �̵� �ӵ�
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
            if (curState == CurrentState.trace) // �߰� ���¿����� ��θ� �����մϴ�
            {
                // �׺���̼� ��θ� �����մϴ�
                nvAgent.SetDestination(playerTransform.position);
            }
            //5�ʿ� �ѹ�
            yield return new WaitForSeconds(5.0f);
        }
    }
    void Patroling()
    {
        if (nvAgent.remainingDistance < 1f && chkTime < IdleTime && flagIdle) // �����ð����� idle
        {
            chkTime += Time.deltaTime;
            //nvAgent.ResetPath();
        }
        if (chkTime > IdleTime) // idle ����, ���� �̵�
        {
            flagIdle = false;
            Debug.Log("chkTime : " + chkTime);
            chkTime = 0;

            if (nvAgent.remainingDistance < 1f)
            {
                _animator.SetBool("isWalk", true);

                //��������Ʈ ���� ����
                pt = Random.Range(0, m_ptPoints.Length);
                Debug.Log("pt >"+ pt);

                nvAgent.SetDestination(m_ptPoints[pt].position);
                //���⼭ ���߳�?
                if (!nvAgent.pathPending)
                {
                    Debug.Log("pp");
                    if (nvAgent.remainingDistance <= nvAgent.stoppingDistance)
                    {
                        Debug.Log("rm");
                        if (!nvAgent.hasPath || nvAgent.velocity.sqrMagnitude == 0f) // ����
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
        float MaxDistance = 2f;// ������ ����
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
        //�÷��̾� ���
    }
}
