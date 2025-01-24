using System;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class LobbyInit : MonoBehaviourPunCallbacks
{
        private string gameVersion = "1";


        private void Awake()
        {
                PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        { 
                Connect();
        }

        public void Connect()
        {
                if (PhotonNetwork.IsConnected)
                {
                        PhotonNetwork.JoinRandomRoom();
                }
                else
                {
                        PhotonNetwork.ConnectUsingSettings();
                        PhotonNetwork.GameVersion = gameVersion;
                }
        }

        public override void OnConnectedToMaster()
        {
                Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
                Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
                Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

                // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
                PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        public override void OnJoinedRoom()
        {
                Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }
}