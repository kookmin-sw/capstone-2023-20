using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//김기범 - Asuna용도 컨트롤러 클래스 (player 확장용 클래스)
public class ThirdPlayerController : MonoBehaviour
{
    //포톤뷰 객체
    private PhotonView pv;
    //보통상태 시네머신 카메라
    private CinemachineVirtualCamera virtualCamera;
    //에임상태일때 시네머신카메라
    private CinemachineVirtualCamera aimVirtualCamera;
    //카메라 root
    private GameObject cameraRoot;
    //카메라 
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();


    private StarterAssetsInputs playerInputs;
    private ThirdPersonController thirdPersonController;
    private Animator animator;


    float hitDistance = 2f;
    public bool InvestigateValue;

    Material outline;

    Renderer renderers;
    List<Material> materialList = new List<Material>();

    Vector3 mouseWorldPosition = Vector3.zero;

    private void Start()
    {
        outline = new Material(Shader.Find("Custom/OutLine"));
    }

    private void Awake()
    {

        pv = GetComponent<PhotonView>();

        //CinemachineVirtualCamera[] cameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        //foreach (CinemachineVirtualCamera camera in cameras)
        //{
        //    if (camera.Name == "AsunaFollowCamera") virtualCamera = camera;
        //    else if (camera.Name == "AsunaAimCamera") aimVirtualCamera = camera;
        //}
        //cameraRoot = GameObject.FindGameObjectWithTag("CinemachineTarget");
        //// 자신의 로컬 캐릭터의 경우 시네머신 카메라 연결
        //if (pv.IsMine)
        //{
        //    virtualCamera.Follow = cameraRoot.transform;
        //    aimVirtualCamera.Follow = cameraRoot.transform;
        //}
        playerInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //화면 중앙 2차원 벡터값
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        //ray오브젝트 카메라에서 마우스가 가르키는 화면포인트를 ray객체에 할당
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {

            // raycast 거리가 2f 이내, 화면에 UI없을시에만 활성화
            // 
            if (hit.distance < hitDistance && EventSystem.current.IsPointerOverGameObject() == false)
            {
                //Debug.Log("충돌객체:" + hit.collider.name);
                // 이벤트 오브젝트 일시
                if (hit.collider.tag == "EventObj")
                {
                    // 상호작용 버튼 활성화
                    InvestigateValue = true;

                    // 상호작용 메세지 활성화
                    Popup.instance.OpenPopUp();

                    // 상호작용시 퍼즐 활성화
                    if (playerInputs.investigate == true)
                    {
                        GameObject.Find(hit.collider.name).GetComponent<Puzzle>().Activate();

                    } 
                    // 키보드 R키 입력 시
                    //if (Input.GetKeyDown(KeyCode.R))
                    //{
                    //    Puzzle.target1();
                    //    //GameObject.Find("Puzzle2").GetComponent<Activate1>().Activate();
                    //}
                }
                else
                {
                    InvestigateValue = false;
                    Popup.instance.ClosePopUp();

                }
            }
            // raycast에 물체가 없을 시 
            else
            {
                InvestigateValue = false;
                Popup.instance.ClosePopUp();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EventObj")
        {
            if (playerInputs.investigate)
            {
                other.GetComponent<EventObject>().getEventUI().SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EventObj")
        {
            other.GetComponent<EventObject>().getEventUI().SetActive(false);
        }
    }


}

