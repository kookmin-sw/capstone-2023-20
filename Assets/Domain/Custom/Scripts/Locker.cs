using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public GameObject PadLock;
    public GameObject InstantPadLock;
    public bool Viewing = false;
    public bool IsLock = false;

    public void LockView()
    {
        if (!Viewing && !IsLock)
        {
            InstantPadLock = Instantiate(PadLock, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,180,0)));
            IsLock= true;
            Viewing= true;
        }
    }

    public void DestroyView()
    {
        Destroy(InstantPadLock);
        IsLock= false;
    }
}
