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
            Map userMap = GetMap(true);
            _user.Init(userMap);
            _enemy.Init(GetMap(false), userMap);
        }

        private Map GetMap(bool isUser)
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

                        }
                    }
                }
            }
            return new Map(cells);
        }

        /*private List<ShipDeck> GetShip(ShipDeck deck)
        {

        }

        private bool TryGetNewNeighbourDeck((int i, int j) indexesDeck)
        {
            (int i, int j)[] shifts = new(int i, int j)[4]
            { 
                (-1, -1),
                (-1, 1),
                (1, -1),
                (1, 1)
            };

            for(int i = 0; i < shifts.Length; i++)
            {
                (int i, int j) newIndex = (indexesDeck.i + shifts[i].i, indexesDeck.j + shifts[i].j);

            }
        }*/
    }
}