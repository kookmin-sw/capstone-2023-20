using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bExit : MonoBehaviour
{

    private StarterAssetsInputs playerInputs;
    private ThirdPlayerController ThirdPlayerController;
    public Puzzle puzzle;

    // Start is called before the first frame update
    private void Awake()
    {
        playerInputs = FindObjectOfType<StarterAssetsInputs>();
        ThirdPlayerController = FindObjectOfType<ThirdPlayerController>();
    }

    public void OnClickExit()
    {
        Debug.Log(playerInputs);
        playerInputs.PlayerLockOn();
        ThirdPlayerController.InvestigateValue = false;
        puzzle.GetComponent<Puzzle>().Activate();
        
    }   
   
}
