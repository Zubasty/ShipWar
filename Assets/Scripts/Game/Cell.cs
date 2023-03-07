using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Cell
    {
        private ShipDeck _deck;
        private bool _isOpen;
        private bool _belongUser;

        public event Action<Cell> TookHit;
        public event Action<Cell> Opened;

        public bool HaveDeck => _deck != null;
        public ShipDeck Deck => _deck;
        public bool IsOpen => _isOpen;
        public bool BelongUser => _belongUser;

        public Cell(bool belongPlayer, ShipDeck deck = null)
        {
            _deck = deck;
            _isOpen = false;
            _belongUser = belongPlayer;
        }

        public void TakeHit()
        {
            TookHit?.Invoke(this);
        }

        public void Open()
        {
            _isOpen = true;
            Opened?.Invoke(this);
        }
    }
}

