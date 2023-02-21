using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CellVisual : MonoBehaviour
    {
        [SerializeField] private Color[] _colorsCondition; //0 - закрыто; 1 - пусто; 2 - палуба; 3 - пусто и подбито; 4 - палуба и подбито
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
        }

        private void OnOpened(Cell cell)
        {
            if (cell.HaveDeck)
                _renderer.color = _colorsCondition[4];
            else
                _renderer.color = _colorsCondition[3];
        }
    }
}