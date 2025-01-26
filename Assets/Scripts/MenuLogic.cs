using System;
using System.Collections.Generic;
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


    [SerializeField] private GameObject createBtnGO;
    [SerializeField] private GameObject backFromLobbyBtnGO;
    [SerializeField] private GameObject backFromLobbyListBtnGO;
    [SerializeField] private GameObject getRoomsBtnGO;
    [SerializeField] private GameObject scrollViewGO;
    [SerializeField] private GameObject joinRoomBtnPrefab;
    [SerializeField] private GameObject contentGO;
    [SerializeField] private GameObject startGameBtn;
    [SerializeField] private TMP_Text playersText;
    [SerializeField] private GameObject waitTextGO;
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        
    }

    
    public void CreateLobby()
    {
        createBtnGO.SetActive(false);
        getRoomsBtnGO.SetActive(false);
        backFromLobbyBtnGO.SetActive(true);
        var roomOptions = new RoomOptions
        {
            MaxPlayers = maxPlayers
        };
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    public void BackFromLobby()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        backFromLobbyBtnGO.SetActive(false);
        createdRoomField.gameObject.SetActive(false);
        startGameBtn.gameObject.SetActive(false);
        createBtnGO.SetActive(true);
        getRoomsBtnGO.SetActive(true);
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        createBtnGO.SetActive(true);
        getRoomsBtnGO.SetActive(true);
    }

    public override void OnCreatedRoom()
    {
        createdRoomField.gameObject.SetActive(true);
        createdRoomField.text = PhotonNetwork.CurrentRoom.Name;
        startGameBtn.SetActive(true);
        playersText.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You Joined Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Joined Room");
        playersText.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount;
        
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("GameScene");
    }
    public void JoinRoom()
    {
        Debug.Log("Joining Room");
        PhotonNetwork.JoinRoom(roomToJoinField.text);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        scrollViewGO.SetActive(false);
        waitTextGO.SetActive(true);
        
    }
    

    public void GetRoomsList()
    {
        createBtnGO.SetActive(false);
        getRoomsBtnGO.SetActive(false);
        backFromLobbyListBtnGO.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public void BackFromLobbyList()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        backFromLobbyListBtnGO.SetActive(false);
        scrollViewGO.SetActive(false);
        createBtnGO.SetActive(true);
        getRoomsBtnGO.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room List updated");
        if (roomList.Count == 0)
        {
            createBtnGO.SetActive(true);
            getRoomsBtnGO.SetActive(true);
            backFromLobbyListBtnGO.SetActive(false);
        }
        else
        {
            if (getRoomsBtnGO.activeSelf) return;
            scrollViewGO.SetActive(true);
            for (var i = 0; i < roomList.Count; i++)
            {
                var btn = Instantiate(joinRoomBtnPrefab, contentGO.transform);
                btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30 * i);
                btn.GetComponentInChildren<TMP_Text>().text = roomList[i].Name;
                var i1 = i;
                btn.GetComponent<Button>().onClick.AddListener(()=> JoinRoom(roomList[i1].Name));

            }
        }
    }
}