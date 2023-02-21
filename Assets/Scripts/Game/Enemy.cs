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
                for(int j = 0; j < _userMap.GetLength(); j++)
                {
                    if (_userMap[i,j].IsOpen == false)
                    {
                        cells.Add(_userMap[i,j]);
                    }
                }
            }

            cells[Random.Range(0, cells.Count)].TakeHit();
        }
    }
}
