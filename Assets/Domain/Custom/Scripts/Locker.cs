using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


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
            Debug.Log("Here");
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
}
