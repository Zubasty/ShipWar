using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ShipVisual : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _destroyedVisual;

        public void Init(Ship ship)
        {
            ship.Destroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _renderer.sprite = _destroyedVisual;
        }
    }
}

