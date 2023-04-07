using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearObj : MonoBehaviour
{
    public GameObject obj;
    public float appearTime = 5f;
<<<<<<< Updated upstream
=======
    public float plusTime = 5f;
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
<<<<<<< Updated upstream
        Invoke("Appear", appearTime); // n초뒤 함수 호출
=======
        Invoke("Appear", appearTime); // n초뒤 함수
        Invoke("Disappear", appearTime*2); // n초뒤 함수 호출
        Invoke("Appear", appearTime*2+3);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Appear()
    {
        obj.SetActive(true);
<<<<<<< Updated upstream
=======

    }
    void Disappear()
    {
        obj.SetActive(false);

>>>>>>> Stashed changes
    }
}
