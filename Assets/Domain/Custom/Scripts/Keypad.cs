using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using TMPro;

public class Keypad : MonoBehaviour
{
    public int[] KeypadArray = new int[6];
    public int counter = 0;
    public bool KeypadClosed = false;
    private string keyname;
    [SerializeField] private GameObject KeyInputs;
    public bool KeypadUnlocked = false;


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
    public void NextStage()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
        int nextLevel = (int)cp["CurrentLevel"] + 1;
        if (cp.ContainsKey("CurrentLevel")) cp.Remove("CurrentLevel"); //충돌 방지 확실하게 삭제후 업데이트 하기 위함;
        cp.Add("CurrentLevel", nextLevel);
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
        if (PhotonNetwork.IsMasterClient)
        {
            LoadingSceneController.LoadScene();
        }
    }
    public void ConfirmKey()
    {
        if (KeypadArray[0] == 6 && KeypadArray[1] == 9 && KeypadArray[2] == 8 && KeypadArray[3] == 2 && KeypadArray[4] == 2 && KeypadArray[5] == 1)
        {
            Debug.Log("Door Unlocked!");
            KeypadUnlocked = true;
            
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
            KeyInputs.transform.Find(keyname).GetComponent<TextMeshProUGUI>().text = "_";

        }
    }

    public void CloseKeypad()
    {
        this.gameObject.SetActive(false);
        KeypadClosed = true;
    }

    public void ReadyClosed()
    {
        KeypadClosed = false;
    }

    public bool IfClosed()
    {
        return KeypadClosed;
    }
}
