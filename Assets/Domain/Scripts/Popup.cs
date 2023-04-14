using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popup;

    private void Awake()
    { 

    }

    public void OpenPopUp()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }
    public void ClosePopUp()
    {
        if (popup)
        {
            popup.SetActive(false);
        }
    }


}
