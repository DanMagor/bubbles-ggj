using System;
using Photon.Pun;
using UnityEngine;

public class SnitchSystemSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _snitchPrefab;
    [SerializeField] private int _count = 10;
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        for (var i = 0; i < _count; i++)
        {
            var go = PhotonNetwork.Instantiate(_snitchPrefab.name, Vector3.zero, Quaternion.identity);
            go.GetComponent<SnitchLogic>()._boxArea = GetComponentInChildren<BoxCollider>();
        }
    }
}