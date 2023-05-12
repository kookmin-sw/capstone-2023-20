//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Activate1 : MonoBehaviour
//{
//    public bool state;
//    public GameObject target;
//    // Start is called before the first frame update
//    void Start()
//    {
//        state = false;
//    }
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            Debug.Log("exit");
//            if (state)
//                Deactivate();
//        }
//    }

//    // Update is called once per frame
//    public void Activate()
//    {
//        target.SetActive(true);
//        print("생겨나");
//        state = true;
//    }

//    public void Deactivate()
//    {
//        target.SetActive(false);
//        print("사라져");
//        state = false;
//    }
//}
