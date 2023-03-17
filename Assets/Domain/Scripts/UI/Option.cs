using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public void OnclickOutBtn()
    {
        Debug.Log("Options.Out " + GameObject.Find("GameManager").name);
        if(GameObject.Find("GameManager")!=null)
            GameObject.Find("GameManager").GetComponent<NetworkManager>().OnClickOutBtn();
    }
}
