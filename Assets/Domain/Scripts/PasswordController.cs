using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PasswordController : MonoBehaviour
{
    public PhotonView pv;
    public TMP_InputField passwordInputField;
    private ObjectManager objectmanager;

    [PunRPC]
    void SyncFunc1()
    {
        objectmanager.SyncActivate();
    }
    private void Awake()
    {
        objectmanager = GetComponent<ObjectManager>();
        pv = GameObject.FindWithTag("Latifa").GetComponent<PhotonView>();
    }

    public void CheckPassword()
    {
        string password = passwordInputField.text;

        if (password == "91912399")
        {
            Debug.Log("암호가 일치합니다!");
            pv.RPC("SyncFunc", RpcTarget.All, "display1");

        }
        else
        {
            Debug.Log("암호가 일치하지 않습니다.");
        }
    }
}