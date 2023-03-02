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
using UnityEngine.SceneManagement;
using System.Threading;
using System;
    

//김기범 - Asuna용도 컨트롤러 클래스 (player 확장용 클래스)
public class ThirdPlayerController : MonoBehaviour
{
    //포톤뷰 객체
    private PhotonView pv;
    //보통상태 시네머신 카메라
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    //카메라 root
    [SerializeField]
    private GameObject cameraRoot;
    //카메라 
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    //김원진 - 인벤토리 GameObject 추가
    [SerializeField] private GameObject Inventory;
    //김원진 - 인벤토리 관리자 InventoryManager 추가
    [SerializeField] private InventoryManager InventoryManager;
    //김원진 - 미니맵 GameObject 추가
    [SerializeField] private GameObject Minimap;
    //김원진 - 현재 캐릭터가 위치한 곳 저장
    [SerializeField] private GameObject CurrentMap;

    [SerializeField] private GameObject TextInteraction;

    private StarterAssetsInputs playerInputs;
    private ThirdPersonController thirdPersonController;
    private Animator animator;


    float hitDistance = 2f;
    public bool InvestigateValue = false;

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
        //KB - 객체생성시 카메라 우선도 조정, 자신의 로컬캐릭터면 우선도 높힘
        pv = GetComponent<PhotonView>();
        if (pv.IsMine) virtualCamera.Priority = 20;
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

        // 유성현
        if (Physics.Raycast(ray, out hit))
        {

            // raycast 거리가 2f 이내, 화면에 UI없을시에만 활성화
            // 
            if (hit.distance < hitDistance && EventSystem.current.IsPointerOverGameObject() == false)
            {
                //Debug.Log("충돌객체: " + hit.collider.name  + "\n충돌태그: " + hit.collider.tag);
                // 퍼즐 오브젝트 일시
                if (hit.collider.CompareTag("PuzzleObj"))
                {
                    // 상호작용 버튼 활성화
                    InvestigateValue = true;

                    // 상호작용 메세지 활성화
                    Popup.instance.OpenPopUp();

                    // 상호작용시 퍼즐 활성화
                    if (playerInputs.investigate == true)
                    {
                        GameObject.Find(hit.collider.name).GetComponent<Puzzle>().Activate();
                        playerInputs.investigate = true;
                        playerInputs.PlayerLockOn();
                    } 

                    // 키보드 R키 입력 시
                    //if (Input.GetKeyDown(KeyCode.R))
                    //{
                    //    Puzzle.target1();
                    //    //GameObject.Find("Puzzle2").GetComponent<Activate1>().Activate();
                    //}
                }
                else if (hit.collider.CompareTag("EventObj"))
                {
                    Popup.instance.OpenPopUp();
                    if (playerInputs.investigate == true)
                    {
                        GameObject.Find(hit.collider.name).GetComponent<DoorOpen>().Activate();

                    }
                }
                else
                {
                    Popup.instance.ClosePopUp();

                }
            }
            // raycast에 물체가 없을 시 
            else
            {
                Popup.instance.ClosePopUp();
            }
        }


        //김원진 - 인벤토리 상태시 인벤토리 UI 활성화
        //김원진 - 중복 UI 방지 위해 미니맵 UI 비활성 코드 추가
        if (playerInputs.inventory)
        {
            Inventory.SetActive(true);
        }
        else
        {
            Inventory.SetActive(false);
        }

        //김원진 - 미니맵 상태시 미니맵 UI 활성화
        //김원진 - 중복 UI 방지 위해 인벤토리 UI 비활성 코드 추가
        //김원진 - 현재 UI가 중복되진 않으나 Update 함수 특성상 그 순서에 따라 Map -> Inventory시
        //MapUI에서 InventoryUI로 UI가 전환되나 Inventory -> Map으로는 전환되진 않음. 
        //상의후 필요시 Inventory -> Map 전환기능 추가 구현 혹은 전환을 아예 막는 방향으로 갈것.
        if (playerInputs.minimap)
        {
            Minimap.SetActive(true);
        }
        else
        {
            Minimap.SetActive(false);
        }

    }

    //김원진 - 미니맵 전환 구역 진입시 미니맵 전환.
    //김원진 - CurrentMap이 Null 상태일 경우 최초 진입한 전환구역의 TransMap을 CurrentMap으로 설정.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Maps")
        {
            Debug.Log("Entering : " + other);
            if (CurrentMap == null)
            {
                GameObject TransMap = other.GetComponent<MinimapTransition>().getMap();
                TransMap.SetActive(true);
                CurrentMap = TransMap;
            }
            if(other.name == "Stair2")
            {
                if (CurrentMap.name == "Floor1")
                {
                    CurrentMap.SetActive(false);
                    GameObject TransMap = other.GetComponent<MinimapTransition>().getMap();
                    TransMap.SetActive(true);
                    CurrentMap = TransMap;
                }
                
            }
            if(other.name == "Stair1")
            {
                if(CurrentMap.name == "Floor2")
                {
                    CurrentMap.SetActive(false);
                    GameObject TransMap = other.GetComponent<MinimapTransition>().getMap();
                    TransMap.SetActive(true);
                    CurrentMap = TransMap;
                }
            }
        }
    }


    //김원진 - 능동적 아이템 획득을 위해 OnTriggerEnter -> OnTriggerStay로 변환
    //       - cf) Enter로 할 시 최초진입이 기준이므로 아이템 획득을 위해 상호작용 버튼을 누를시 획득되지 않는 경우가 생김. 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Items")
        {
            TextInteraction.SetActive(true);
            //김원진 - 아이템 상호작용시 습득
            if (playerInputs.interaction)
            {
                //김원진 - 상호작용시 떠있는 EventUI 문구 제거.
                TextInteraction.SetActive(false);
                Debug.Log(other.GetComponent<ItemController>().Item);
                InventoryManager.addItem(other.GetComponent<ItemController>().Item);
                other.GetComponent<GetItem>().Get();

                //Debug.Log("Item:" + other.GetComponent<ItemController>().Item);
                
                playerInputs.interaction = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Items")
        {
            TextInteraction.SetActive(false);
        }
        if (other.tag == "Maps")
        {
            Debug.Log("Exiting : " + other);
            if (other.name == "Stair2")
            {
                if (CurrentMap.name == "Floor1")
                {
                    CurrentMap.SetActive(false);
                }

            }
        }
        if (other.tag == "Maps")
        {
            if (other.name == "Stair1")
            {
                if (CurrentMap.name == "Floor2")
                {
                    CurrentMap.SetActive(false);
                }

            }
        }
    }


}

