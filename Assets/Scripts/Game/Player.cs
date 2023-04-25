using System;
using UnityEngine;

namespace Game
{
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] private Vector2 _positionMap;
        [SerializeField] private CellVisual _cellPrefab;
        [SerializeField] private ShipVisual[] _shipPrefab;

        private Map _map;
        private CellVisual[,] _cellsVisual;
        private Ship[] _ships;
        private ShipVisual[] _shipsVisual;

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
            _shipsVisual = new ShipVisual[_ships.Length];

            for(int i = 0; i < _shipsVisual.Length; i++)
            {
                _shipsVisual[i] = Instantiate(_shipPrefab[_ships[i].CountDecks - 1], transform);
                _shipsVisual[i].transform.position = new Vector3(_positionMap.x, _positionMap.y, 0) + 
                    new Vector3(_ships[i].Indexes.i, _ships[i].Indexes.j, _shipsVisual[i].transform.localPosition.z);
                _shipsVisual[i].Init(_ships[i]);
            }

            _cellsVisual = new CellVisual[_map.GetLength(), _map.GetLength()];

            for (int i = 0; i < _map.GetLength(); i++)
            {
                for (int j = 0; j < _map.GetLength(); j++)
                {
                    _cellsVisual[i, j] = Instantiate(_cellPrefab, transform);
                    _cellsVisual[i, j].transform.position = _positionMap + new Vector2(i, j);
                    ShipVisual shipVisual = null;

                    for(int k = 0; k < _shipsVisual.Length; k++)
                        if (_shipsVisual[k].HaveDeck(_map[i, j].Deck))
                            shipVisual = _shipsVisual[k];

                    _cellsVisual[i, j].Init(_map[i, j], _map.IsLeftOrDownDeck(i, j), shipVisual);
                }
            }
        }

        public void Init(Map map, Ship[] ships)
        {
            _map = map;
            _ships = ships;
        }

        private void OnTookHit(Cell cell)
        {
            if(cell.IsOpen == false)
            {
                TakingAttack?.Invoke(cell, this);
                TookAttack?.Invoke(cell, this);
            }
        }
    }
}