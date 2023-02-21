using UnityEngine;

namespace Game
{
    public class MapGameGenerator : MonoBehaviour
    {
        [SerializeField] private MapsSO _maps;
        [SerializeField] private User _user;
        [SerializeField] private Enemy _enemy;

        private void Awake()
        {
            Map userMap = GetMapUser();
            _user.Init(userMap);
            _enemy.Init(GetMapEnemy(), userMap);
        }

        private Map GetMapUser()
        {
            Cell[,] cells = new Cell[_maps.GetLengthMaps(), _maps.GetLengthMaps()];

            for (int j = 0; j < cells.GetLength(0); j++)
            {
                for (int i = 0; i<cells.GetLength(1); i++)
                {
                    if(_maps.GetCellUser(i,j) == 1)
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell(true, new ShipDeck());
                    }
                    else
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell(true);
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
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell(false, new ShipDeck());
                    }
                    else
                    {
                        cells[i, _maps.GetLengthMaps() - j - 1] = new Cell(false);
                    }
                }
            }

            return new Map(cells);
        }
    }
}