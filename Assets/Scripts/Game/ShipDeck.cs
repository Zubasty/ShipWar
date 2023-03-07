using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ShipDeck 
    { 
        public Ship Ship { get; private set; }

        public void Init(Ship ship)
        {
            Ship = ship;
        }
    }
}

