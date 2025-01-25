using UnityEngine;

public class BonusMassAndSize : MonoBehaviour
{
    [SerializeField] private float _bonusMass = 1f;
    [SerializeField] private float _bonusSize = 0.5f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            var rb = col.gameObject.GetComponent<Rigidbody>();
            rb.mass += _bonusMass;
            col.gameObject.transform.localScale += Vector3.one * _bonusSize;
            gameObject.SetActive(false);
        }
    }
}
