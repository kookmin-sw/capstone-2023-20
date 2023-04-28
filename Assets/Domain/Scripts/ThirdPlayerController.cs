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


//���� - Asuna�뵵 ��Ʈ�ѷ� Ŭ���� (player Ȯ��� Ŭ����)
public class ThirdPlayerController : MonoBehaviour
{
    //포톤뷰 객체
    private PhotonView pv;
    //보통상태 시네머신 카메라
    public CinemachineVirtualCamera virtualCamera;
    //카메라 root
    [SerializeField]
    private GameObject cameraRoot;
    //카메라
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    //KKB - Option
    [SerializeField] private GameObject Option;
    //김원진 - 인벤토리 GameObject 추가
    [SerializeField] private GameObject Inventory;
    //김원진 - 인벤토리 관리자 InventoryManager 추가
    [SerializeField] private InventoryManager InventoryManager;
    //김원진 - 미니맵 GameObject 추가
    [SerializeField] private GameObject Minimap;
    //김원진 - 현재 캐릭터가 위치한 곳 저장
    [SerializeField] private GameObject CurrentMap;

    [SerializeField] private GameObject UnlockInteraction;
    [SerializeField] private GameObject LockView;
    [SerializeField] private GameObject ItemView;
    [SerializeField] private GameObject CCTVView;
    private StarterAssetsInputs playerInputs;
    private ThirdPersonController thirdPersonController;
    private Animator animator;
    private bool CurrentDoorLock = false;
    private bool isCollision = false;

    float hitDistance = 2f;
    public bool InvestigateValue = false;

    Vector3 mouseWorldPosition = Vector3.zero;

    // 팝업창
    private Popup popup;

    private void Start()
    {
    }

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        playerInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        popup = GetComponentInChildren<Popup>();
    }
    private void Update()
    {

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ������
        if (Physics.Raycast(ray, out hit))
        {

            // raycast 2f 이내, 화면에 UI없을시에만 활성화
            if (hit.distance < hitDistance )
            {
                //Debug.Log("충돌객체: " + hit.collider.name  + "\n충돌태그: " + hit.collider.tag);
                // 퍼즐 오브젝트 일시
                if (hit.collider.CompareTag("PuzzleObj"))
                {

                    // 상호작용 메시지 활성화
                    popup.OpenPopUpInteract();
                    if (playerInputs.investigate == true)
                    {
                        InvestigateValue = true;
                        GameObject.Find(hit.collider.name).GetComponent<Puzzle>().Activate();
                        playerInputs.PlayerLockOn();
                    }

                }
                else if (hit.collider.CompareTag("EventObj"))
                {
                    popup.OpenPopUpInteract();
                    // 김원진 - 잠긴 문인지 확인
                    if (playerInputs.investigate == true)
                    {
                        if (CurrentDoorLock == false)
                        {
                            // 유성현 - UnityEvent Invoke를 이용해 서로 다른 함수를 호출 할 수 있도록 확장
                            GameObject.Find(hit.collider.name).GetComponent<ObjectManager>().Activate();
                            playerInputs.investigate = false;
                            playerInputs.interaction = false;
                        }
                    }

                }
                else if (hit.collider.CompareTag("LockerUnlocked"))
                {
                    popup.OpenPopUpInteract();
                    if (playerInputs.investigate == true)
                    {
                        // 유성현 - UnityEvent Invoke를 이용해 서로 다른 함수를 호출 할 수 있도록 확장
                        GameObject.Find(hit.collider.name).GetComponent<Cabinet>().Activate();
                        playerInputs.investigate = false;
                        playerInputs.interaction = false;
                    }
                }
                else if (hit.collider.CompareTag("Items"))
                {
                    popup.OpenPopUpItem();
                    //김원진 - 아이템 상호작용시 습득
                    if (playerInputs.interaction)
                    {
                        //김원진 - 상호작용시 떠있는 EventUI 문구 제거.
                        InventoryManager.addItem(hit.collider.GetComponent<ItemController>().Item);
                        hit.collider.GetComponent<GetItem>().Get();
                        playerInputs.interaction = false;
                    }
                }
                else if (hit.collider.CompareTag("LockerItem"))
                {
                    popup.OpenPopUpItem();
                    if (playerInputs.investigate == true)
                    {
                        //ItemInteraction.SetActive(false);
                        popup.ClosePopUpItem();
                        Debug.Log(hit.collider.GetComponent<ItemController>().Item);
                        InventoryManager.addItem(hit.collider.GetComponent<ItemController>().Item);
                        hit.collider.GetComponent<GetItem>().Get();
                        playerInputs.interaction = false;
                    }


                }
                else if (hit.collider.CompareTag("Locker"))
                {
                    //popup.OpenPopUpInteract();
                }
                
                else
                {
                    popup.ClosePopUpInteract();
                    popup.ClosePopUpItem();
                }
            }
            // raycast에 물체가 없을 시
            else
            {
                popup.ClosePopUpInteract();
                popup.ClosePopUpItem();
            }
            if (isCollision == false)
            {
                playerInputs.interaction = false;
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
        //KKB - option Input
        //if (playerInputs.option)
        //{
        //    Option.SetActive(true);
        //}
        //else
        //{
        //    Option.SetActive(false);
        //}

    }


    //김원진 - 미니맵 전환 구역 진입시 미니맵 전환.
    //김원진 - CurrentMap이 Null 상태일 경우 최초 진입한 전환구역의 TransMap을 CurrentMap으로 설정.
    private void OnTriggerEnter(Collider other)
    {
        isCollision = true;
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

        if (other.tag == "Locker")
        {
            popup.OpenPopUpInteract();
            Debug.Log("Locker Interaction");
            if (playerInputs.interaction)
            {
                if (other.GetComponent<Locker>().IsLock == false)
                {
                    other.GetComponent<Locker>().LockView();
                    other.GetComponent<Locker>().InstantPadLock.transform.parent = ItemView.transform;
                    GameObject InstantPadLock = ItemView.transform.Find("Combination PadLock(Clone)").gameObject;
                    InstantPadLock.transform.localPosition = new Vector3(0, 0, 0.1f);
                    InstantPadLock.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

                    LockView.SetActive(true);
                    playerInputs.UILock = true;
                    playerInputs.PlayerMoveLock();
                }

                playerInputs.interaction = false;
                popup.ClosePopUpInteract();
            }
            if (!LockView.activeSelf)
            {
                playerInputs.UILock = false;
                playerInputs.PlayerMoveUnlock();
                other.GetComponent<Locker>().Viewing = false;
            }
            if (other.GetComponent<Locker>().unLock == true)
            {
                LockView.SetActive(false);
                popup.OpenPopUpInteract();
                other.gameObject.transform.Find("door").gameObject.tag = "LockerUnlocked";
                popup.ClosePopUpInteract();
                //김원진 - Locker 주변에서 UI 켰을경우
                if (playerInputs.inventory)
                {
                    playerInputs.UILock = true;
                }
                if (playerInputs.minimap)
                {
                    playerInputs.UILock = true;
                }
            }



        }
        else if (other.tag == "CCTV")
        {
            popup.OpenPopUpInteract();
            if (playerInputs.interaction)
            {
                CCTVView.SetActive(true);
                playerInputs.UILock = true;
                playerInputs.PlayerMoveLock();
                popup.ClosePopUpInteract();
                playerInputs.interaction = false;
            }

            if (!CCTVView.activeSelf)
            {
                playerInputs.UILock = false;
                playerInputs.PlayerMoveUnlock();
            }
        }
        else if (other.tag == "LockedDoor")
        {
            CurrentDoorLock = other.GetComponent<DoorLock>().getDoorState();
            if (playerInputs.interaction)
            {
                if (InventoryManager.Items.Find(x => x.ItemName == "Announce Room Key"))
                {
                    UnlockInteraction.SetActive(true);
                    other.GetComponent<DoorLock>().DoorUnlock();
                    CurrentDoorLock = other.GetComponent<DoorLock>().getDoorState();
                    InventoryManager.removeItem("Announce Room Key");
                    other.gameObject.transform.parent.gameObject.GetComponent<DoorDefaultClose>().UnLockDoor();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCollision= false;
        if (other.tag == "Items")
        {
            //ItemInteraction.SetActive(false);
            popup.ClosePopUpItem();
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
        if (other.tag == "Locker")
        {
            other.GetComponent<Locker>().DestroyView();
            //popup.ClosePopUpInteract();
            LockView.SetActive(false);
        }
        if (other.tag == "CCTV")
        {
            popup.ClosePopUpInteract();
        }

        if (other.tag == "LockedDoor")
        {
            UnlockInteraction.SetActive(false);
            CurrentDoorLock = false;
        }
    }


}
