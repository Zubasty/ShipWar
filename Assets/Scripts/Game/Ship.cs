using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Ship : MonoBehaviour
    {
        private ShipDeck[] _decks;

        public void Init(ShipDeck[] decks)
        {
            _decks = decks;
        }
    }
}