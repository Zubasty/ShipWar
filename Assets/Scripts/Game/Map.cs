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
        }

        public int GetLength() => _cells.GetLength(0);

        public Cell this[int i, int j] => _cells[i, j];
    }
}