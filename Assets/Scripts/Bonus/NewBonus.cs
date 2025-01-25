using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Bonus
{
    public abstract class NewBonus : MonoBehaviour
    {
        public abstract void ActivateBonus(GameObject target);
    }
}