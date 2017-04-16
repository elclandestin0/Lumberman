using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Network : MonoBehaviour 
{
    public GameObject[] spawnSpots;
    public Transform spawnPosition;
    private const string roomName = "RoomName";
    private RoomInfo[] roomsList;
	// Use this for initialization
	void Start () 
    {
        spawnSpots = GameObject.FindGameObjectsWithTag("Positions");
        Connect();
	}
	
    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("version 1337");
        //PhotonNetwork.offlineMode = true;
    }

    void OnGUI()
    {
         GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        //else if (PhotonNetwork.room == null)
        // {
        //// Create Room

        //     if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
        //     {
        //         RoomOptions roomOptions = new RoomOptions()
        //         {
        //             isVisible = true,
        //             isOpen = false,
        //             maxPlayers = 5
        //         };
        //         PhotonNetwork.CreateRoom(roomName + Guid.NewGuid().ToString("N"), roomOptions, TypedLobby.Default);

        //     }
               
        
                
        //// Join Room
        //     if (roomsList != null)
        //     {
        //        for (int i = 0; i < roomsList.Length; i++)
        //        {
        //            if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].Name))
        //                 PhotonNetwork.JoinRoom(roomsList[i].Name);
        //        }   
        //     }
        // }
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("Room is in list");
        roomsList = PhotonNetwork.GetRoomList();
    }

    void OnConnectedToMaster()
    {
        //PhotonNetwork.CreateRoom(null);
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnJoinedLobby()
    {
        
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }


    void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Connected To Room");
        SpawnAPlayer();
    }

    void SpawnAPlayer()
    {
        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("Player 1", spawnPosition.position, Quaternion.identity, 0);
        myPlayer.GetComponent<PlayerMovement>().enabled = true;
    }
	// Update is called once per frame
	void Update () 
    {
		
	}
}
