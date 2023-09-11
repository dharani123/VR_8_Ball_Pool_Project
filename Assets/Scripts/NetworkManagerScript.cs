using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class NetworkManagerScript : MonoBehaviourPunCallbacks
{
    // This class handles network things like Create Server, Join Room, Disconnect Server and Leave Room.
    public GameObject panel;
    public GameObject RoomsUI;
    public TMP_InputField roomName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to the Server");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conncted to Master.");
        base.OnConnectedToMaster();
        panel.SetActive(false);
        RoomsUI.SetActive(true);
    }

    public void JoinRoom()
    {

        RoomOptions roomOptions = new RoomOptions();

        PhotonNetwork.LoadLevel(2);
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered the room. Say Hello");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
