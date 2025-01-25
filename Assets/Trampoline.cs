using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline Settings")]
    [SerializeField] private float _launchForce = 10f;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            var rb = col.rigidbody;
            rb.velocity = Vector3.zero;
            Vector3 launchDirection = transform.up;
            rb.AddForce(launchDirection * _launchForce, ForceMode.Impulse);
        }
    }
}
