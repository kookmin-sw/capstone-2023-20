using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEntryController : MonoBehaviour
{
    public string objectToActivateName = "Fog"; // 활성화할 오브젝트의 이름을 입력합니다.

    [SerializeField] private GameObject objectToActivate; // 활성화할 오브젝트를 저장하기 위한 변수

    //private void Start()
    //{
    //    objectToActivate = GameObject.Find(objectToActivateName); // 이름에 해당하는 오브젝트를 찾아 저장합니다.

    //    if (objectToActivate == null)
    //    {
    //        Debug.LogError("Object with the name " + objectToActivateName + " not found!");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Taichi"))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}
