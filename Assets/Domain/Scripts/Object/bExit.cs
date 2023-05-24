using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bExit : MonoBehaviour
{

    private StarterAssetsInputs playerInputs;
    private ThirdPlayerController ThirdPlayerController;
    public Puzzle puzzle;
    private CanvasRenderModeChanger changer;

    // Start is called before the first frame update
    private void Awake()
    {
        playerInputs = FindObjectOfType<StarterAssetsInputs>();
        ThirdPlayerController = FindObjectOfType<ThirdPlayerController>();
        changer = puzzle.GetComponent<CanvasRenderModeChanger>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("exit");
            CallExit();
        }
    }

    public void OnClickExit()
    {
        CallExit();
        
    }   
    public void CallExit()
    {
        puzzle.GetComponent<Puzzle>().Activate();
        if (changer != null)
            changer.Activate();
    }
   
}
