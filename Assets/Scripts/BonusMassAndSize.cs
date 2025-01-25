using UnityEngine;

public class BonusMassAndSize : MonoBehaviour
{
    public float _additionalHP = 5f;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHP>().GetHP(_additionalHP);
            Destroy(gameObject);
        }
    }
}
