using System;
using UnityEngine;

namespace Game
{
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] private Vector2 _positionMap;
        [SerializeField] private CellVisual _cellPrefab;

        private Map _map;
        private CellVisual[,] _cellsVisual;

        public event Action<Cell,Player> TakingAttack;
        public event Action<Cell,Player> TookAttack;

        public bool IsLose => _map.IsAllDeckAttacked;

        private void OnEnable()
        {
            for (int i = 0; i < _map.GetLength(); i++)
            {
                for (int j = 0; j < _map.GetLength(); j++)
                {
                    _map[i, j].TookHit += OnTookHit;
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _map.GetLength(); i++)
            {
                for (int j = 0; j < _map.GetLength(); j++)
                {
                    _map[i, j].TookHit -= OnTookHit;
                }
            }
        }

        private void Start()
        {
            _cellsVisual = new CellVisual[_map.GetLength(), _map.GetLength()];

            for (int i = 0; i < _map.GetLength(); i++)
            {
                for (int j = 0; j < _map.GetLength(); j++)
                {   
                    _cellsVisual[i, j] = Instantiate(_cellPrefab, transform);
                    _cellsVisual[i, j].transform.position = _positionMap + new Vector2(i, j);
                    _cellsVisual[i, j].Init(_map[i, j]);
                }
            }
        }

        public void Init(Map map)
        {
            _map = map;
        }

        private void OnTookHit(Cell cell)
        {
            if(cell.IsOpen == false)
            {
                TakingAttack?.Invoke(cell,this);
                TookAttack?.Invoke(cell, this);
            }
        }
    }
}