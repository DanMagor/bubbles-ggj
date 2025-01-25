using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnitchLogic : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _speed = 10f;
    private Bounds _bounds;
    [SerializeField] public Collider _boxArea;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _bounds = _boxArea.bounds;
            _targetPos = GetRandomTarget();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(_targetPos, transform.position) < 0.1f)
            if (PhotonNetwork.IsMasterClient)
            {
                
                _targetPos = GetRandomTarget();
                photonView.RPC("SetTarget", RpcTarget.All, _targetPos);
            }

        transform.position += (_targetPos - transform.position).normalized * (_speed * Time.deltaTime);
    }

    private Vector3 GetRandomTarget()
    {
        return new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            0.5f,
            Random.Range(_bounds.min.z, _bounds.max.z)
        );
    }

    [PunRPC]
    private void SetTarget(Vector3 pos)
    {
        _targetPos = pos;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(_targetPos);
        }
        else
        {
            // Network player, receive data
            _targetPos = (Vector3)stream.ReceiveNext();
        }
    }
}