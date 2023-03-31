using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearObj : MonoBehaviour
{
    public GameObject obj;
    public float appearTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
        Invoke("Appear", appearTime); // n�ʵ� �Լ� ȣ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Appear()
    {
        obj.SetActive(true);
    }
}
