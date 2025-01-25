using System;
using System.Linq;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class GameInit : MonoBehaviour
{

  
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private Material[] playersMaterials;
  [SerializeField] private CinemachineVirtualCamera _camera;

  private void Awake()
  {
    GameManager.Players = PhotonNetwork.PlayerList.ToList();
  }

  private void Start()
  {
    for (var i = 0; i < GameManager.Players.Count; i++)
    {
      if (GameManager.Players[i].ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber) continue;
      var go = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0+(1.5f*i), 2, 0), Quaternion.identity);
      _camera.Follow = go.transform;
      break;
    }
  }
}