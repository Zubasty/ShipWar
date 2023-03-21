using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Ship
    {
        private ShipDeck[] _decks;
        
        public (float i, float j) Indexes { get; }

        public ShipDeck this[int i] => _decks[i];

        public event Action Destroyed;

        public Ship(ShipDeck[] decks, (float i, float j) indexes)
        {
            _decks = decks;
            Indexes = indexes;

            foreach(ShipDeck deck in _decks)
            {
                deck.Init(this);
                deck.TookDamage += OnTookDamage;
            }
        }

        private void OnTookDamage()
        {
            foreach (ShipDeck deck in _decks)
            {
                if(deck.IsDamaged == false)
                {
                    return;
                }
            }

            Destroyed?.Invoke();
        }

        ~Ship()
        {
            foreach (ShipDeck deck in _decks)
            {
                deck.TookDamage -= OnTookDamage;
            }
        }
    }
}

