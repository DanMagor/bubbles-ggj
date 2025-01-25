using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline Settings")]
    [SerializeField] private float _launchForce = 10f;
    [SerializeField] private float _stunDuration = 0.5f;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            var rb = col.rigidbody;
            var stun = rb.gameObject.GetComponent<PlayerController>();
            var stunDecrease = rb.mass * 0.1f;
            if (stunDecrease < _stunDuration)
            {
                stun.StunPlayer(_stunDuration - stunDecrease);
            }
            rb.velocity = Vector3.zero;
            Vector3 launchDirection = transform.up;
            rb.AddForce(launchDirection * _launchForce, ForceMode.Impulse);
        }
        
    }
}
