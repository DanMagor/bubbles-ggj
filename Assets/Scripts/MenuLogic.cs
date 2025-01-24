using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviourPunCallbacks
{
    
    private string gameVersion = "1";
    [SerializeField] private int maxPlayers = 4;
    [SerializeField] private TMP_InputField createdRoomField;
    [SerializeField] private TMP_InputField roomToJoinField;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    
    public void CreateLobby()
    {
        var roomOptions = new RoomOptions
        {
            MaxPlayers = maxPlayers
        };
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.LogError("Connected to master");
    }

    public override void OnCreatedRoom()
    {
        createdRoomField.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnJoinedRoom()
    {
        Debug.LogError("You Joined Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogError("Player Joined Room");
        Debug.LogError("TOTAL NUM PLAYERS " + PhotonNetwork.CurrentRoom.Players.Count);
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.Players.Count > 1)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void JoinRoom()
    {
        Debug.LogError("Joining Room");
        PhotonNetwork.JoinRoom(roomToJoinField.text);
    }
}