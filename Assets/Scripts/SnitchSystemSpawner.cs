using Photon.Pun;
using UnityEngine;

public class SnitchSystemSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _massBufferSnitchPrefab;
    [SerializeField] private GameObject _spikedSnitchPrefab;
    [SerializeField] private int _massCount = 10;
    [SerializeField] private int _spikeCount = 10;
    [SerializeField] private BoxCollider _box;
    private void Awake()
    {
        _box = GetComponentInChildren<BoxCollider>();
        if (!PhotonNetwork.IsMasterClient) return;
        for (var i = 0; i < _massCount; i++)
        {
            var massBuffer = PhotonNetwork.Instantiate(_massBufferSnitchPrefab.name, Vector3.zero, Quaternion.identity);
            massBuffer.GetComponent<SnitchLogic>()._boxArea = _box;
        }
        for (var i = 0; i < _spikeCount; i++)
        {
            var spike = PhotonNetwork.Instantiate(_spikedSnitchPrefab.name, Vector3.zero, Quaternion.identity);
            spike.GetComponent<SnitchLogic>()._boxArea = _box;
        }
    }
}