using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Map
    {
        private Cell[,] _cells;

        public Map(Cell[,] cells)
        {
            _cells = cells;

            for(int i = 0; i < _cells.GetLength(0); i++)
            {
                for(int j =0; j<_cells.GetLength(1); j++)
                {
                    _cells[i, j].Opened += OnOpened;
                }
            }
        }

        public int GetLength() => _cells.GetLength(0);

        public Cell this[int i, int j] => _cells[i, j];

        private void OnOpened(Cell cell)
        {
            if (cell.HaveDeck)
            {

            }
        }

        private bool CanOpenEnvironment(Cell cell, Cell cellDefault = null)
        {
            return true; //необходимо дописать алгоритм
        }

        ~Map()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Opened -= OnOpened;
                }
            }
        }
    }
}