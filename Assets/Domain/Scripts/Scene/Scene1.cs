using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1 : MonoBehaviour
{
    public bool State;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && State == false)
        {
            SceneManager.LoadScene("cutScene", LoadSceneMode.Additive);
            State = true;
        }
    }
}
