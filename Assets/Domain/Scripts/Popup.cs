using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popupInteract;
    public GameObject popupItem;

    private void Awake()
    { 

    }

    public void OpenPopUpInteract()
    {
        if (popupItem != null)
        {
            popupInteract.SetActive(true);
        }
    }
    public void ClosePopUpInteract()
    {
        if (popupItem)
        {
            popupInteract.SetActive(false);
        }
    }
    public void OpenPopUpItem()
    {
        if (popupItem != null)
        {
            popupItem.SetActive(true);
        }
    }
    public void ClosePopUpItem()
    {
        if (popupItem)
        {
            popupItem.SetActive(false);
        }
    }


}
