using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Vector2 _positionMap;
        [SerializeField] private CellVisual _cellPrefab;

        private Map _map;
        private CellVisual[,] _cellsVisual;

        private void Start()
        {
            _cellsVisual = new CellVisual[_map.GetLength(), _map.GetLength()];

            for (int i = 0; i < _map.GetLength(); i++)
            {
                for (int j = 0; j < _map.GetLength(); j++)
                {
                    _cellsVisual[i, j] = Instantiate(_cellPrefab, transform);
                    _cellsVisual[i, j].transform.position = _positionMap + new Vector2(i, j);
                    _cellsVisual[i, j].Init(_map[i,j], CellVisualCondition.Close);
                }
            }
        }

        public void Init(Map map)
        {
            _map = map;
        }
    }
}
