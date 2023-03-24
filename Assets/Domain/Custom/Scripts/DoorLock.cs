using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    private bool IsDoorLocked = true;

    public bool getDoorState()
    {
        return IsDoorLocked;
    }

    public void DoorUnlock()
    {
        IsDoorLocked= false;
    }
}
