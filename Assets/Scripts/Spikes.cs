using Photon.Realtime;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private float _damage = 5f;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(_playerTag))
        {
            col.gameObject.GetComponent<PlayerHP>().TakeDamage(_damage);
            if (gameObject.CompareTag("Snitch"))
            {
                Destroy(gameObject);
            }
        }
    }
}
