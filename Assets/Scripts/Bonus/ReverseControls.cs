using System;
using UnityEngine;

namespace Bonus
{
    public class ReverseControls : NewBonus
    {
        [SerializeField] private Material bonusMaterial;
        public override void ActivateBonus(GameObject target)
        {
            var pc = target.gameObject.GetComponent<PlayerController>();
            pc.InverseControl();
        }
    }
}