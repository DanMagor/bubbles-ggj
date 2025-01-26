using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Material[] playersMaterials;
    [SerializeField] private CinemachineVirtualCamera _camera;


    [SerializeField] private Transform[] playersSpawnPositions;
    private List<Player> playersList;
    private List<PlayerController> playersControllers;

    [SerializeField] private GameObject _restartBtn;

    private void Awake()
    {
        playersList = PhotonNetwork.PlayerList.ToList();
        playersControllers = new List<PlayerController>();

        if (PhotonNetwork.IsMasterClient)
        {
            _restartBtn.SetActive(true);
        }
    }

    private void Start()
    {
        for (var i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber) continue;
            var go = PhotonNetwork.Instantiate(playerPrefab.name, playersSpawnPositions[i].position,
                Quaternion.identity);
            playersControllers.Add(go.GetComponent<PlayerController>());
            _camera.Follow = go.transform;
            break;
        }
    }

    public void KillPlayer(PhotonView playerView)
    {
        for (var i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].ActorNumber != playerView.Controller.ActorNumber) continue;
            playersControllers[i].KillPlayer();
            break;
        }
    }

    public void RestartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.DestroyAll();
        PhotonNetwork.LoadLevel("RestartScene");
    }
}