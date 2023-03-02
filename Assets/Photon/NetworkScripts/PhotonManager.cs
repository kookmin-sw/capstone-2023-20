using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; 

//KB - PhotonManger class
public class PhotonManager : MonoBehaviourPunCallbacks //�ݹ��Լ� override�� ���� ���
{
    //����
    private readonly string version = "1.0f";
    // ����� ���̵� �Է�
    private string userId = "Tester";

    private void Awake()
    {   
        //�ӽ� ����ȭ��
        Screen.SetResolution(960, 540, false);
        //���� ���� �����鿡�� �ڵ����� ���� �ε�
        PhotonNetwork.AutomaticallySyncScene = true;
        //���� ������ �������� �����ϵ��� ����
        PhotonNetwork.GameVersion = version;
        //���� id �ʱ�ȭ
        PhotonNetwork.NickName = userId;
        //photon server�� ��� Ƚ�� ����, �ʴ� 30
        Debug.Log(PhotonNetwork.SendRate);

        //server in
        PhotonNetwork.ConnectUsingSettings();

    }

    // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // ==> false
        //�κ� ����
        PhotonNetwork.JoinLobby(); 
    }

    //�κ� ����� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // == > true

        PhotonNetwork.JoinRandomRoom(); // ���� ��ġ ����� �ϴ� �Լ�
        //TODO : �濡�ִ� ĳ���� ������ ���� �濡 ������ ĳ���Ͱ� �ٸ����� ����

    }

    //������ �� ������ �������� ��� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}:{message}");

        //�� �Ӽ� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; //�ִ� ������ ��
        roomOptions.IsOpen = true; //�� ���¿���
        roomOptions.IsVisible = true; //�κ񿡼� �� ��Ͽ� ���̰�����������

        //�� ����
        PhotonNetwork.CreateRoom("Test Room", roomOptions);
    }
    // �� ������ �Ϸ�� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Test Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }
    //�뿡 ���� �� ȣ��Ǵ� �ݹ� �Լ�

    public override void OnJoinedRoom()
    {
        
        Debug.Log($"PhotonNetwork.InRoom = { PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        //�뿡 ������ ����� �̸�
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            //�뿡 �ִ� �����̸��̶� ������ȣ Ȯ��
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}");
        }
        //TODO : �濡�ִ� ĳ���Ͱ� �ƽ��� ���� �ѾȽ�¾�(�ӽ�)�� ���� �ν���Ʈȭ�� ������ �ٲ���ߵ�
        //�濡������ �ν���Ʈȭ�� resourcse ���� prefabs�� string, ���Ͱ�, ȸ���� �־ ����
        PhotonNetwork.Instantiate("Player2", new Vector3(-136.33f, 3.63f, 26.319f), Quaternion.identity);
        //GameObject.Find("Plyaer").SetActive(true);

    }

}
