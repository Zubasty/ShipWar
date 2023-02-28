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

        public bool IsAllDeckAttacked
        {
            get
            {
                foreach(Cell cell in _cells)
                {
                    if(cell.HaveDeck && cell.IsOpen == false)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public Cell this[int i, int j] => _cells[i, j];

        public int GetLength() => _cells.GetLength(0);

        public bool HaveCell(int i, int j, out Cell cell)
        {
            bool have = i >= 0 && j >= 0 && i < _cells.GetLength(0) && j < _cells.GetLength(1);

            if (have)
            {
                cell = _cells[i, j];
            }
            else
            {
                cell = null;
            }

            return have;
        }


        private void OnOpened(Cell cell)
        {
            if (cell.HaveDeck && CanOpenEnvironment(cell))
            {
                OpenEnvironment(cell);
            }
        }

        private void OpenEnvironment(Cell cell, Cell defaultCell = null)
        {
            (int i, int j) indexes = GetIndexesCell(cell);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (HaveCell(indexes.i + i, indexes.j + j, out Cell openingCell) && openingCell != defaultCell && openingCell != cell)
                    {
                        if (openingCell.HaveDeck)
                        {
                            if (openingCell.IsOpen)
                            {
                                OpenEnvironment(openingCell, cell);
                            }
                            else
                            {
                                throw new System.Exception($"ћы пытаемс€ открыть €чейки вокруг неподбитой палубы в €чейке {openingCell}");
                            }
                        }
                        else if(openingCell.IsOpen == false)
                        {
                            openingCell.Open();
                        }
                    }
                }
            }
        }

        private bool CanOpenEnvironment(Cell cell, Cell cellDefault = null)
        {
            (int i, int j) indexes = GetIndexesCell(cell);
            List<Cell> cellsIsOpenHaveDeck = new List<Cell>();

            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if(HaveCell(indexes.i + i, indexes.j + j, out Cell checkingCell) && checkingCell != cell 
                        && checkingCell != cellDefault && checkingCell.HaveDeck)
                    {
                        if (checkingCell.IsOpen)
                        {
                            cellsIsOpenHaveDeck.Add(checkingCell);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            if(cellsIsOpenHaveDeck.Count > 0)
            {
                bool b = true;

                foreach(Cell cellForCheck in cellsIsOpenHaveDeck)
                {
                    b = b && CanOpenEnvironment(cellForCheck, cell);
                }

                return b;
            }
            else
            {
                return true;
            }
        }


        private (int, int) GetIndexesCell(Cell cell)
        {
            for(int i = 0; i<_cells.GetLength(0); i++)
            {
                for(int j = 0; j<_cells.GetLength(1); j++)
                {
                    if(cell == _cells[i, j])
                    {
                        return (i, j);
                    }
                }
            }

            throw new System.Exception($"ячейки {cell} не существует на карте {this}");
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