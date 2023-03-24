using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public GameObject PadLock;
    public GameObject InstantPadLock;
    public GameObject LockerContent;
    public bool Viewing = false;
    public bool IsLock = false;
    public bool unLock = false;
    public void LockView()
    {
        if (!Viewing && !IsLock)
        {
            InstantPadLock = Instantiate(PadLock, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,180,0)));
            InstantPadLock.GetComponent<PadLockPassword>().setLocker(this);
            IsLock= true;
            Viewing= true;
        }
    }

    public void DestroyView()
    {
        Destroy(InstantPadLock);
        IsLock= false;
    }

    public void Unlock()
    {
        Debug.Log("Unlocked");
        unLock = true;
        DestroyView();
        Destroy(this.gameObject.transform.Find("Combination PadLock").gameObject);
        // 캐비넷 상호작용 추가
    }
}
