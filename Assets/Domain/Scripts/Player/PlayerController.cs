using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity = 40; //40 �̻��̸� ���� ����


    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;


    public Camera theCamera;

    [SerializeField]
    private Rigidbody myRigid;

    //private Animator anim;




    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();

    }




    // Update is called once per frame, 1�ʿ� 60�� ȣ�� �뷫
    void Update()
    {

        Move();
        CameraRotation();
        CharacterRotation();

    }

    private void Move()
    {

        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX; //1,0,0 / -1,0,0
        Vector3 _moveVertical = transform.forward * _moveDirZ;//0,0,1, / 0,0,-1

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        //time.deltatime�� �� 0.016��

        //if(_moveDirX != 0 || _moveDirZ != 0)
        //{
        //    anim.SetBool("isWalk", true);
        //}
        //else
        //{
        //    anim.SetBool("isWalk", false);
        //}
    }


    private void CharacterRotation()
    {
        // �¿� ĳ���� ȸ��
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        //Debug.Log(myRigid.rotation);
        //Debug.Log(myRigid.rotation.eulerAngles);
    }

    private void CameraRotation()
    {
        // ���� ī�޶� ȸ��
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX; // ����
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        // ���� ī�޶�ȸ������ (-�Ѱ谪,�Ѱ谪) ���̿����� �����̵���

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        
    }

}