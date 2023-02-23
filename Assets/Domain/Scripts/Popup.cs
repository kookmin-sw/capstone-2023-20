using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popup;
    public static Popup instance { get; private set; }

    private void Awake()
    {
        if (null == instance)
            instance = this;

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
