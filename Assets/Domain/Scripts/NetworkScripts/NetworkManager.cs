using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Cockpit.Forms;
using StarterAssets;

public class NetworkManager : MonoBehaviourPunCallbacks { 
    public  PhotonView pv;
    private GameObject LocalPlayer;
    [SerializeField]
    private GameObject gameOverMsg;
    private void Awake()
    {
       
    }

    public void OnLevelWasLoaded(int level)
    {
        if(level ==1 ) 
        {
            CreatePlayer(PhotonNetwork.LocalPlayer);

        }
    }
    private void CreatePlayer(Player player)
    {
        Transform pos = GameObject.Find("SpwanPoint" + player.NickName).transform;
        LocalPlayer = PhotonNetwork.Instantiate("Player" + player.NickName, pos.position, pos.rotation);

        //로컬플레이어의 starterAssetsInputs을 Option에 주입;
        /*LocalPlayer.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Option>().SetInputSystem(LocalPlayer.GetComponent<StarterAssetsInputs>());*/ 
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        gameOverMsg.SetActive(true);
        Invoke("LeaveRoom",3f);
    }

    public void LeaveRoom()
    {
        Debug.Log("게임씬나가기");
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }


  
}
