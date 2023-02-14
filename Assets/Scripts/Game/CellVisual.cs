using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CellVisual : MonoBehaviour
    {
        [SerializeField] private Color[] _colorsCondition;
        [SerializeField] private SpriteRenderer _renderer;

        private CellVisualCondition _condition;
        private Cell _cell;

        private void OnMouseDown()
        {
            if(_condition == CellVisualCondition.Close)
            {
                if(_cell.HaveDeck)
                {
                    _condition = CellVisualCondition.Damaged;
                }
                else
                {
                    _condition = CellVisualCondition.Clear;
                }

                SetColor();
            }  
        }

        public void Init(Cell cell, CellVisualCondition condition)
        {
            _cell = cell;
            _condition = condition;
            SetColor();
        }

        private void SetColor()
        {
            _renderer.color = _colorsCondition[(int)_condition];
        }
    }
}