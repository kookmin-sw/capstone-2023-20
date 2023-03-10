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
    //����� ��ü
    private PhotonView pv;
    //������� �ó׸ӽ� ī�޶�
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    //ī�޶� root
    [SerializeField]
    private GameObject cameraRoot;
    //ī�޶� 
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    //����� - �κ��丮 GameObject �߰�
    [SerializeField] private GameObject Inventory;
    //����� - �κ��丮 ������ InventoryManager �߰�
    [SerializeField] private InventoryManager InventoryManager;
    //����� - �̴ϸ� GameObject �߰�
    [SerializeField] private GameObject Minimap;
    //����� - ���� ĳ���Ͱ� ��ġ�� �� ����
    [SerializeField] private GameObject CurrentMap;

    [SerializeField] private GameObject ItemInteraction;
    [SerializeField] private GameObject LockInteraction;
    [SerializeField] private GameObject LockView;
    [SerializeField] private GameObject ItemView;

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
        //KB - ��ü������ ī�޶� �켱�� ����, �ڽ��� ����ĳ���͸� �켱�� ����
        pv = GetComponent<PhotonView>();
        if (pv.IsMine) virtualCamera.Priority = 20;
        playerInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //ȭ�� �߾� 2���� ���Ͱ�
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        //ray������Ʈ ī�޶󿡼� ���콺�� ����Ű�� ȭ������Ʈ�� ray��ü�� �Ҵ�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ������
        if (Physics.Raycast(ray, out hit))
        {

            // raycast �Ÿ��� 2f �̳�, ȭ�鿡 UI�����ÿ��� Ȱ��ȭ
            // 
            if (hit.distance < hitDistance && EventSystem.current.IsPointerOverGameObject() == false)
            {
                //Debug.Log("�浹��ü: " + hit.collider.name  + "\n�浹�±�: " + hit.collider.tag);
                // ���� ������Ʈ �Ͻ�
                if (hit.collider.CompareTag("PuzzleObj"))
                {

                    // ��ȣ�ۿ� �޼��� Ȱ��ȭ
                    Popup.instance.OpenPopUp();

                    // ��ȣ�ۿ�� ���� Ȱ��ȭ
                    if (playerInputs.investigate == true)
                    {
                        // ��ȣ�ۿ� ��ư Ȱ��ȭ
                        InvestigateValue = true;
                        GameObject.Find(hit.collider.name).GetComponent<Puzzle>().Activate();
                        playerInputs.PlayerLockOn();
                    } 

                    // Ű���� RŰ �Է� ��
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
                        playerInputs.investigate = false;
                        playerInputs.interaction = false;
                    }
                }
                else
                {
                    Popup.instance.ClosePopUp();

                }
            }
            // raycast�� ��ü�� ���� �� 
            else
            {
                Popup.instance.ClosePopUp();
            }
        }


        //����� - �κ��丮 ���½� �κ��丮 UI Ȱ��ȭ
        //����� - �ߺ� UI ���� ���� �̴ϸ� UI ��Ȱ�� �ڵ� �߰�
        if (playerInputs.inventory)
        {
            Inventory.SetActive(true);
        }
        else
        {
            Inventory.SetActive(false);
        }

        //����� - �̴ϸ� ���½� �̴ϸ� UI Ȱ��ȭ
        //����� - �ߺ� UI ���� ���� �κ��丮 UI ��Ȱ�� �ڵ� �߰�
        //����� - ���� UI�� �ߺ����� ������ Update �Լ� Ư���� �� ������ ���� Map -> Inventory��
        //MapUI���� InventoryUI�� UI�� ��ȯ�ǳ� Inventory -> Map���δ� ��ȯ���� ����. 
        //������ �ʿ�� Inventory -> Map ��ȯ��� �߰� ���� Ȥ�� ��ȯ�� �ƿ� ���� �������� ����.
        if (playerInputs.minimap)
        {
            Minimap.SetActive(true);
        }
        else
        {
            Minimap.SetActive(false);
        }

    }

    //����� - �̴ϸ� ��ȯ ���� ���Խ� �̴ϸ� ��ȯ.
    //����� - CurrentMap�� Null ������ ��� ���� ������ ��ȯ������ TransMap�� CurrentMap���� ����.
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
        
        if (other.tag == "Locker")
        {
            LockInteraction.SetActive(true);
        }
    }


    //����� - �ɵ��� ������ ȹ���� ���� OnTriggerEnter -> OnTriggerStay�� ��ȯ
    //       - cf) Enter�� �� �� ���������� �����̹Ƿ� ������ ȹ���� ���� ��ȣ�ۿ� ��ư�� ������ ȹ����� �ʴ� ��찡 ����. 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Items")
        {
            ItemInteraction.SetActive(true);
            //����� - ������ ��ȣ�ۿ�� ����
            if (playerInputs.interaction)
            {
                //����� - ��ȣ�ۿ�� ���ִ� EventUI ���� ����.
                ItemInteraction.SetActive(false);
                Debug.Log(other.GetComponent<ItemController>().Item);
                InventoryManager.addItem(other.GetComponent<ItemController>().Item);
                other.GetComponent<GetItem>().Get();
                playerInputs.interaction = false;
            }
        }
        else if (other.tag == "Locker")
        {
            if(playerInputs.interaction)
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
                LockInteraction.SetActive(false);
            }
            if (!LockView.activeSelf)
            {
                playerInputs.UILock = false;
                playerInputs.PlayerMoveUnlock();
                other.GetComponent<Locker>().Viewing = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Items")
        {
            ItemInteraction.SetActive(false);
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
            LockInteraction.SetActive(false);
            LockView.SetActive(false);
        }
    }


}

