using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    
}
