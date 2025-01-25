using Photon.Realtime;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(_playerTag))
        {
            col.gameObject.GetComponent<PlayerController>().KillPlayer();
        }
    }
}
