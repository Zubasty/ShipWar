using UnityEngine;

namespace Game
{
    public class MapGameGenerator : MonoBehaviour
    {
        [SerializeField] private MapsSO _maps;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;

        private void Awake()
        {
            _player.Init(GetMapPlayer());
            _enemy.Init(GetMapEnemy());
        }

        private Map GetMapPlayer()
        {
            Cell[,] cells = new Cell[_maps.GetLengthMaps(), _maps.GetLengthMaps()];

            for (int j = 0; j < cells.GetLength(0); j++)
            {
                for (int i = 0; i<cells.GetLength(1); i++)
                {
                    if(_maps.GetCellPlayer(i,j) == 1)
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell(new ShipDeck());
                    }
                    else
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell();
                    }
                }
            }

            return new Map(cells);
        }

        private Map GetMapEnemy()
        {
            Cell[,] cells = new Cell[_maps.GetLengthMaps(), _maps.GetLengthMaps()];

            for (int j = 0; j < cells.GetLength(0); j++)
            {
                for (int i = 0; i < cells.GetLength(1); i++)
                {
                    if (_maps.GetEnemyCell(i, j) == 1)
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell(new ShipDeck());
                    }
                    else
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell();
                    }
                }
            }

            return new Map(cells);
        }
    }
}