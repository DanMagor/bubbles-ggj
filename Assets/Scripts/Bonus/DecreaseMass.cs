using UnityEngine;

namespace Bonus
{
    public class DecreaseMass : NewBonus
    {
        [SerializeField] private Material bonusMaterial;
        [SerializeField] private float massStep = 1f;

        public override void ActivateBonus(GameObject target)
        {
            var rb = target.gameObject.GetComponent<Rigidbody>();
            rb.mass -= massStep;
        }
    }
}