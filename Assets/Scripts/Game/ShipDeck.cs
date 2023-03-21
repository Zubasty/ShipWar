using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ShipDeck 
    { 
        public Ship Ship { get; private set; }
        public bool IsDamaged { get; private set; }

        public event Action TookDamage;

        public void Init(Ship ship)
        {
            Ship = ship;
            IsDamaged = false;
        }

        public void TakeDamage()
        {
            IsDamaged = true;
            TookDamage?.Invoke();
        }
    }
}

