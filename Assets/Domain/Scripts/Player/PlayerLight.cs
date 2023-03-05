/*
 * 2019-08-24
 * 
 * 
 *  ����Ƽ ������ ��ũ��Ʈ
 *  FŰ�� ���갡��
 *  �ۼ���: ������
 *  
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Light flash_light;


    Transform tr;
    KeyCode[] KeyCode_List; //Ű�ڵ尪 �ɽ�

    //
    [SerializeField]
    private Camera getCamera;

    void Awake()
    {
        flash_light = GetComponent<Light>();
        tr = this.transform;

        Key_Depoly();
    }

    void Key_Depoly()
    {
        //Ű �迭
        KeyCode_List = new KeyCode[10];

        //�ڵ尪 �ɽ��ϱ�
        KeyCode_List[0] = KeyCode.F;
        KeyCode_List[1] = KeyCode.Escape;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode result = User_Input();
        tr.rotation = getCamera.transform.rotation;


        if (result == KeyCode_List[0])
        {
            if (flash_light.enabled)
            {
                flash_light.enabled = false;
            }
            else
            {
                flash_light.enabled = true;
            }
        }
    }

    //������ ���ȴ°�
    KeyCode User_Input()
    {
        KeyCode result = KeyCode_List[1];

        for (int i = 0; i < KeyCode_List.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode_List[i]))
            {
                result = KeyCode_List[i];
            }
        }

        return result;

    }
}
