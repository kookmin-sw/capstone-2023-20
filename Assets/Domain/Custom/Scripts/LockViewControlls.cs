using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockViewControlls : MonoBehaviour
{
    public void CloseView()
    {
        gameObject.SetActive(false);
    }
}
