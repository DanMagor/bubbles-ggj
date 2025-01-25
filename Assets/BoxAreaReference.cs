using UnityEngine;

public class BoxAreaReference : MonoBehaviour
{
    public static Collider box { get; private set; }


    private void Awake()
    {
        box = gameObject.GetComponent<Collider>();
    }
}
