using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    public int[] KeypadArray = new int[6];
    public int counter = 0;
    private string keyname;
    [SerializeField] private GameObject KeyInputs;


    public void AddKey(int key)
    {
        Debug.Log("Adding!");
        KeypadArray[counter++] = key;
        keyname = "Key " + counter;
        KeyInputs.transform.Find(keyname).GetComponent<TextMeshProUGUI>().text = "*";
        if (counter > 5)
        {
            counter = 0;
        }
    }
    
    public void ConfirmKey()
    {
        if (KeypadArray[0] == 6 && KeypadArray[1] == 9 && KeypadArray[2] == 8 && KeypadArray[3] == 2 && KeypadArray[4] == 2 && KeypadArray[5] == 1)
        {
            Debug.Log("Door Unlocked!");
        }
        else
        {
            Debug.Log("Incorrect Password");
            ResetKey();
        }
    }

    public void ResetKey()
    {
        counter = 0;
        for (int i=0; i<6; i++)
        {
            KeypadArray[i] = 0;
            keyname = "Key " + (i+1);
            KeyInputs.transform.Find(keyname).GetComponent<TextMeshProUGUI>().text = " ";

        }
    }

    public void CloseKeypad()
    {
        this.gameObject.SetActive(false);
    }
}
