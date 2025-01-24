using System;
using Photon.Pun;
using UnityEngine;

public class GameInit : MonoBehaviour, IPunObservable
{

  
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private Material[] playersMaterials;
  private int playersConnected = 0;
  private Vector3 lastPosition;
  private Quaternion lastRotation;
  
  
  private void Start()
  {
    var go = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0+(1.5f*playersConnected), 2, 0), Quaternion.identity);
    playersConnected++;
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
   
  }
}