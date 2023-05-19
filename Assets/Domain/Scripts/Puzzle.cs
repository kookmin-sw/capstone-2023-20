using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using StarterAssets;

public class Puzzle : MonoBehaviour
{
    public bool state;
    public GameObject target;
    public Canvas canvas;
    private ThirdPlayerController ThirdPlayerController;
    private StarterAssetsInputs playerInputs;
    private CanvasRenderModeChanger changer;

    // Start is called before the first frame update
    void Start()
    {
        this.state = false;
        ThirdPlayerController = FindObjectOfType<ThirdPlayerController>();
        playerInputs = FindObjectOfType<StarterAssetsInputs>();
        changer = GetComponent<CanvasRenderModeChanger>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("exit");
            if (state)
            {
                Activate();
                if (changer != null)
                    changer.Activate();

            }

        }
    }

    // Update is called once per frame
    public void Activate()
    {
        if (state == false)
        {
            target.SetActive(true);
            this.state = true;
            playerInputs.PlayerLockOn();
            ThirdPlayerController.InvestigateValue = true;

        }
        else
        {
            target.SetActive(false);
            this.state = false;
            playerInputs.PlayerLockOn();
            ThirdPlayerController.InvestigateValue = false;
        }

    }
    public void ChangeDisplay()
    {
        canvas.targetDisplay = 2;

    }

    public void LockOff()
    {
        playerInputs.PlayerLockOn();
        ThirdPlayerController.InvestigateValue = false;
    }

}
