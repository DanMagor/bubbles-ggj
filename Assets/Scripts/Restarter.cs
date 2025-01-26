using System;
using Photon.Pun;
using UnityEngine;

public class Restarter : MonoBehaviourPun
{

    [SerializeField] private GameObject _clientText;
    [SerializeField] private GameObject _hostText;
    [SerializeField] private GameObject _restartBtn;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _hostText.SetActive(true);
            _restartBtn.SetActive(true);
        }
        else
        {
            _clientText.SetActive(true);
        }
    }


    public void RestartBtn()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("GameScene");
    }
}