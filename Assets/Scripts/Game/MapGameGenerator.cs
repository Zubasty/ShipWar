using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class MapGameGenerator : MonoBehaviour
    {
        [SerializeField] private MapsSO _maps;
        [SerializeField] private User _user;
        [SerializeField] private Enemy _enemy;

        private void Awake()
        {
            Map userMap = GetMap(true, out Ship[] ships);
            _user.Init(userMap, ships);
            _enemy.Init(GetMap(false, out ships), userMap, ships);
        }

        private Map GetMap(bool isUser, out Ship[] ships)
        {
            Cell[,] cells = new Cell[_maps.GetLengthMaps(), _maps.GetLengthMaps()];

            for (int j = 0; j < cells.GetLength(0); j++)
            {
                for (int i = 0; i < cells.GetLength(1); i++)
                {
                    (int i, int j) indexes = (i, _maps.GetLengthMaps() - j - 1);

                    if (_maps.GetCellUser(i, j) == 1)
                    {
                        cells[indexes.i, indexes.j] = new Cell(isUser, new ShipDeck());
                    }
                    else
                    {
                        cells[indexes.i, indexes.j] = new Cell(isUser);
                    }
                }
            }

            List<List<ShipDeck>> listsDeck = new List<List<ShipDeck>>();
            List<(float i, float j)> shipsPosition = new List<(float i, float j)>();

            for (int j = 0; j < cells.GetLength(0); j++)
            {
                for (int i = 0; i < cells.GetLength(1); i++)
                {
                    if (cells[i, j].HaveDeck)
                    {
                        bool haveInListsDeck = false;

                        foreach (List<ShipDeck> decks in listsDeck)
                        {
                            if(decks.Contains(cells[i, j].Deck))
                            {
                                haveInListsDeck = true;
                                break;
                            }
                        }

                        if(haveInListsDeck == false)
                        {
                            listsDeck.Add(GetShip(cells,(i,j), out (float i, float j) positionShip));
                            shipsPosition.Add(positionShip);
                        }
                    }
                }
            }

            ships = new Ship[listsDeck.Count];

            for (int i = 0; i<listsDeck.Count; i++)
            {
                ships[i] = new Ship(listsDeck[i].ToArray(), shipsPosition[i]);
            }

            return new Map(cells);
        }

        private bool HaveCell(Cell[,] cells, (int i, int j) indexesCell)
        {
            return indexesCell.i >= 0 && indexesCell.j >= 0 && indexesCell.i < cells.GetLength(0) && indexesCell.j < cells.GetLength(1);
        }

        private List<ShipDeck> GetShip(Cell[,] cells, (int i, int j) indexesDeck, out (float i, float j) positionShip)
        {
            if(HaveCell(cells, indexesDeck) == false)
                throw new System.Exception($"ячейки с номерами {indexesDeck} не существует");

            List<ShipDeck> ship = new List<ShipDeck>();
            ship.Add(cells[indexesDeck.i, indexesDeck.j].Deck);
            (int i, int j) previousIndexes = (-1, -1);
            (int i, int j) currentIndexes = indexesDeck;
            positionShip = (indexesDeck.i, indexesDeck.j);

            while (TryGetNewNeighbourDeck(cells, currentIndexes, previousIndexes, out (int i, int j) newIndexes))
            {
                ship.Add(cells[newIndexes.i, newIndexes.j].Deck);
                previousIndexes = currentIndexes;
                currentIndexes = newIndexes;
                positionShip.i += currentIndexes.i;
                positionShip.j += currentIndexes.j;
            }

            positionShip.i /= ship.Count;
            positionShip.j /= ship.Count;

            return ship;
        }

        private bool TryGetNewNeighbourDeck(Cell[,] cells, (int i, int j) indexeDefaultsDeck, (int i, int j) indexesPrevousDeck, out (int i, int j) indexesNewDeck)
        {
            (int i, int j)[] shifts = new(int i, int j)[4]
            { 
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
            };

            for(int i = 0; i < shifts.Length; i++)
            {
                (int i, int j) newIndexes = (indexeDefaultsDeck.i + shifts[i].i, indexeDefaultsDeck.j + shifts[i].j);
                
                if(HaveCell(cells, newIndexes) && cells[newIndexes.i, newIndexes.j].Deck != null && newIndexes != indexesPrevousDeck)
                {
                    indexesNewDeck = newIndexes;
                    return true;
                }
            }

            indexesNewDeck = (-1, -1);
            return false;
        }
    }
}