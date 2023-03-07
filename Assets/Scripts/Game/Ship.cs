using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Ship
    {
        private ShipDeck[] _decks;

        public ShipDeck this[int i] => _decks[i];

        public Ship(ShipDeck[] decks)
        {
            _decks = decks;

            foreach(ShipDeck deck in _decks)
            {
                deck.Init(this);
            }
        }
    }
}

