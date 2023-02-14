using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Cell
    {
        private ShipDeck _deck;
        private bool _isHitted;

        public bool HaveDeck => _deck != null;

        public Cell(ShipDeck deck = null)
        {
            _deck = deck;
            _isHitted = false;
        }

        public void TakeHit(out bool HadDamage)
        {
            _isHitted = true;
            HadDamage = HaveDeck;
        }
    }
}

