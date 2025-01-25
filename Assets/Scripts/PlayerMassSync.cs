using System;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMassSync : MonoBehaviour, IPunObservable
{
    private Rigidbody _rigidbody;
    private float mass;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        mass = _rigidbody.mass;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            mass = _rigidbody.mass;
            stream.SendNext(mass);
        }
        else
        {
            _rigidbody.mass = (float)stream.ReceiveNext();
        }
    }
}