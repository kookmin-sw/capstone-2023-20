using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class PasswordController : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    private ObjectManager objectmanager;

    private void Awake()
    {
        objectmanager = GetComponent<ObjectManager>();
    }

    public void CheckPassword()
    {
        string password = passwordInputField.text;

        if (password == "1")
        {
            Debug.Log("암호가 일치합니다!");
            objectmanager.Activate();

        }
        else
        {
            Debug.Log("암호가 일치하지 않습니다.");
        }
    }
}