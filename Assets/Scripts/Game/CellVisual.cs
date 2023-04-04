using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CellVisual : MonoBehaviour
    {
        [SerializeField] private Sprite _destroyedDeckSprite;
        [SerializeField] private ParticleSystem _fireDeck;
        [SerializeField] private SpriteRenderer _fireAndDestroyedRenderer;
        [SerializeField] private Color[] _colorsCondition; //0 - закрыто/не подбито; 1 - подбита€ пуста€ €чейка; 2 - подбитый корабль; 3 - подбита€ отдельна€ палуба.
        [SerializeField] private SpriteRenderer _renderer;

        private Cell _cell;

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
                _cell.Deck.Ship.Destroyed -= OnDestroyedShip;
        }

        public void Init(Cell cell)
        {
            _cell = cell;

            if (cell.BelongUser)
            {
                if (cell.HaveDeck)
                    _renderer.color = _colorsCondition[2];
                else
                    _renderer.color = _colorsCondition[1];
            }
            else
            {
                _renderer.color = _colorsCondition[0];
            }

            gameObject.SetActive(true);

            if(_cell.HaveDeck)
                _cell.Deck.Ship.Destroyed += OnDestroyedShip;
        }

        private void OnOpened(Cell cell)
        {
            if (cell.HaveDeck)
            {
                _fireDeck.gameObject.SetActive(true);
            }           
            else
            {
                _renderer.color = _colorsCondition[3];
            }             
        }

        private void OnDestroyedShip()
        {
            _fireDeck.gameObject.SetActive(false);
            _fireAndDestroyedRenderer.sprite = _destroyedDeckSprite;
        }
    }
}