using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : Player
    {
        [SerializeField] private Map _userMap;

        public void Init(Map map, Map userMap)
        {
            Init(map);
            _userMap = userMap;
        }

        public void Step()
        {
            List<Cell> cells = new List<Cell>();

            for(int i = 0; i < _userMap.GetLength(); i++)
            {
                bool needBreak = false;

                for(int j = 0; j < _userMap.GetLength(); j++)
                {
                    if (_userMap[i,j].HaveDeck && _userMap[i, j].IsOpen)
                    {
                        List<Cell> cellsCloseEnvironmentOpenedDeck = new List<Cell>();

                        for(int iShift = -1; iShift <= 1; iShift++)
                        {
                            for (int jShift = -1; jShift <= 1; jShift++)
                            {
                                if((iShift == 0 || jShift == 0) && _userMap.HaveCell(i+iShift, j+jShift, out Cell cellForAdd))
                                {
                                    if(cellForAdd.IsOpen == false)
                                    {
                                        cellsCloseEnvironmentOpenedDeck.Add(cellForAdd);
                                    }
                                    else if(cellForAdd.HaveDeck)
                                    {
                                        if(iShift == 0)
                                        {
                                            if(jShift == -1)
                                            {
                                                if(_userMap.HaveCell(i, j + 1, out Cell cellUp) && cellUp.IsOpen == false)
                                                {
                                                    cells.Clear();
                                                    cells.Add(cellUp);
                                                    needBreak = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    //„ерез фор проверить все €чейки ниже, выбрать первую свободную
                                                }
                                            }
                                        }
                                        //јналогичными ифами проверить верх, лево, право
                                    }
                                }
                            }
                            if (needBreak)
                            {
                                break;
                            }
                        }
                        if (needBreak)
                        {
                            break;
                        }

                        if(cellsCloseEnvironmentOpenedDeck.Count > 0)
                        {
                            cells = cellsCloseEnvironmentOpenedDeck;
                            needBreak = true;
                            break;
                        }
                    }
                    else if (_userMap[i,j].IsOpen == false)
                    {
                        cells.Add(_userMap[i,j]);
                    }
                }
                if (needBreak)
                    break;
            }

            cells[Random.Range(0, cells.Count)].TakeHit();
        }
    }
}
