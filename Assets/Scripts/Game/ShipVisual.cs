using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class ShipVisual : MonoBehaviour, IRotater
    {
        private const float ValueRotate = -90;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private ParticleSystem _effectBoom;

        private Ship _ship;

        public event Action BlownUp;

        public float DefaultRotateValue => _ship.IsRotated ? ValueRotate : 0;

        private void Start()
        {
            if(_ship.IsRotated)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + ValueRotate);
        }

        private void OnDestroy()
        {
            _ship.Destroyed -= OnDestroyed;
        }

        public void Init(Ship ship)
        {
            _ship = ship;
            _ship.Destroyed += OnDestroyed;
        }

        public bool HaveDeck(ShipDeck deck)
        {
            for(int i = 0; i < _ship.CountDecks; i++)
                if (_ship[i] == deck)
                    return true;
            return false;
        }

        private IEnumerator Booming()
        {
            _effectBoom.Play();
            yield return new WaitForSeconds(_effectBoom.startLifetime/4);
            BlownUp?.Invoke();
        }

        private void OnDestroyed()
        {
            StartCoroutine(Booming());
            _renderer.sprite = null;
        }
    }
}

