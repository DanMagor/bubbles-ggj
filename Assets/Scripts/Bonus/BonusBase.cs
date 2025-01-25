using System;
using UnityEngine;

namespace Bonus
{
    public class BonusBase : MonoBehaviour
    {
        public NewBonus NewBonus;
        public event Action OnBonusCollected;
        private Material mat;

        private void Start()
        {
            mat = gameObject.GetComponent<Material>();
            mat = NewBonus.gameObject.GetComponent<Material>();
        }

        private void OnCollisionEnter(Collision other)
        {
            NewBonus.ActivateBonus(other.gameObject);

            /*if (other.gameObject.CompareTag("Player"))
            {
                NewBonus.ActivateBonus(other.gameObject);
            }*/
        }
    }
}