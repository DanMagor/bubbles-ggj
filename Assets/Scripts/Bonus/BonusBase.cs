using System;
using UnityEngine;

namespace Bonus
{
    public class BonusBase : MonoBehaviour
    {
        public NewBonus NewBonus;
        public event Action OnBonusCollected;
        private MeshRenderer mat;

        private void Start()
        {

            mat = gameObject.GetComponent<MeshRenderer>();
            mat.material = ((IncreaseMass)NewBonus).bonusMaterial;
            

        }
        
        

        private void OnTriggerEnter(Collider other)
        {
            NewBonus.ActivateBonus(other.gameObject);

            /*if (other.gameObject.CompareTag("Player"))
            {
                NewBonus.ActivateBonus(other.gameObject);
            }*/
        }
    }
}