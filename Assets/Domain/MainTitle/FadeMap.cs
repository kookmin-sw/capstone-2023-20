using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMap : MonoBehaviour
{
    FadeManager fade;
    void Start()
    {
        fade = FindObjectOfType<FadeManager>();

        fade.FadeOut();
    }


    void Update()
    {
        
    }
}
