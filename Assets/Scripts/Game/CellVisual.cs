using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CellVisual : MonoBehaviour
    {
        [SerializeField] private SettingsDestroyedDecksSO _middleDestroyedSettings;
        [SerializeField] private SettingsDestroyedDecksSO _bowDestroyedSettings;
        [SerializeField] private SettingsDestroyedDecksSO _emptyDestroyedSettings;
        [SerializeField] private ParticleSystem _fireDeck;
        [SerializeField] private SpriteRenderer _fireAndDestroyedRenderer;
        [SerializeField] private SpriteRenderer _renderer;

        private Sprite _destroyedDeckSprite;

        private Cell _cell;
        private ShipVisual _shipVisual;

        public bool HaveDeck => _cell.HaveDeck;

        private void OnEnable()
        {
            _cell.Opened += OnOpened;
        }

        private void OnDisable()
        {
            _cell.Opened -= OnOpened;
        }

        private void OnMouseDown()
        {
            if(_cell.BelongUser == false)
            {
                _cell.TakeHit();
            }  
        }

        private void OnDestroy()
        {
            if (_cell.HaveDeck)
                _shipVisual.BlownUp -= OnBlownUp;
        }

        public void Init(Cell cell, bool isLeftOrDownDeck, ShipVisual shipVisual = null)
        {
            _cell = cell;
            _shipVisual = shipVisual;

            gameObject.SetActive(true);

            _destroyedDeckSprite = isLeftOrDownDeck ? _bowDestroyedSettings.SpriteDestroyed : _middleDestroyedSettings.SpriteDestroyed;
            bool isMirrored = isLeftOrDownDeck ? _bowDestroyedSettings.IsMirrored : _middleDestroyedSettings.IsMirrored;

            if (isMirrored)
                _fireAndDestroyedRenderer.transform.localScale = new Vector3(-_fireAndDestroyedRenderer.transform.localScale.x,
                        _fireAndDestroyedRenderer.transform.localScale.y, _fireAndDestroyedRenderer.transform.localScale.z);

            float rotateValue = isLeftOrDownDeck ? _bowDestroyedSettings.Rotation : _middleDestroyedSettings.Rotation;
            if (_cell.HaveDeck)
                rotateValue += _cell.Deck.Ship.IsRotated ? -90 : 0;
            _fireAndDestroyedRenderer.transform.localEulerAngles += new Vector3(0, 0, rotateValue);

            if (_cell.HaveDeck)
                _shipVisual.BlownUp += OnBlownUp;
        }

        private void OnOpened(Cell cell)
        {
            if (cell.HaveDeck)
            {
                _fireDeck.gameObject.SetActive(true);
            }           
            else
            {
                _fireAndDestroyedRenderer.sprite = _emptyDestroyedSettings.SpriteDestroyed;
            }             
        }

        private void OnBlownUp()
        {
            _fireDeck.gameObject.SetActive(false);
            _fireAndDestroyedRenderer.sprite = _destroyedDeckSprite;
        }
    }
}