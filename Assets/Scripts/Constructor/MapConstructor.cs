using UnityEngine;
using System;

public class MapConstructor : MonoBehaviour
{
    [SerializeField] private CellConstructor _cell;
    [SerializeField] private int _size = 10;
    [SerializeField] private Vector2 _shift;

    private CellConstructor[,] _cells;

    private void Start()
    {
        _cells = new CellConstructor[_size, _size];

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _cells[i, j] = Instantiate(_cell, transform);
                _cells[i, j].transform.position = new Vector2(i, j) + _shift;
            }
        }
    }

    public void Take(ShipDeckConstructor deck)
    {
        (int i, int j)[] pairs = new (int, int)[_size * _size];

        for (int i = 0; i<_size; i++)
        {
            for (int j = 0; j<_size; j++)
            {
                pairs[i*_size+j] = (i, j);
            }
        }

        for (int i = 0; i < pairs.Length; i++)
        {
            int number = UnityEngine.Random.Range(0, pairs.Length);
            (int, int) el = pairs[i];
            pairs[i] = pairs[number];
            pairs[number] = el;   
        }
        
        for (int i = 0; i < pairs.Length; i++)
        {        
            CellConstructor cell = _cells[pairs[i].i, pairs[i].j];

            if(CanTakeDeck(deck, cell))
            {
                Take(deck, cell);
                break;
            }
        }
    }

    public void Take(ShipDeckConstructor deck, CellConstructor cell)
    {
        if (CanTakeDeck(deck, cell) == false)
            throw new Exception($"¬ €чейку {cell} установить корабль {deck.Ship} палубой {deck} невозможно");

        bool goToRight = deck.Ship.IsRotated;
        int countDecks = deck.Ship.CountDecks;
        int numberDeck = deck.Ship.GetNumberDeck(deck);

        for (int i = 0; i < countDecks; i++)
        {
            (int i, int j) indexes = GetIndexes(cell);
            
            if (goToRight)
            {
                indexes.i += i - numberDeck;
            }
            else
            {
                indexes.j += i - numberDeck;
            }
            _cells[indexes.i, indexes.j].InstallDeck(deck.Ship[i]);
        }

        if(deck.Ship.IsInstalled == true)
        {
            deck.Ship.Deinstall();
        }

        deck.Ship.Install(deck, cell.transform.position);
    }

    public bool CanTakeDeck(ShipDeckConstructor deck, CellConstructor cell)
    {
        bool goToRight = deck.Ship.IsRotated;
        int countDecks = deck.Ship.CountDecks;
        int numberDeck = deck.Ship.GetNumberDeck(deck);     

        for (int i = 0; i < countDecks; i++)
        {
            (int i, int j) indexes = GetIndexes(cell);

            if (goToRight)
            {
                indexes.i += i - numberDeck;
            }
            else
            {
                indexes.j += i - numberDeck;
            }

            if ((HaveCell(indexes.i, indexes.j) && IsFreeCell(indexes)) == false)
            {
                Debug.Log($" орабль нельз€ установить в €чейку {indexes}, потому что она зан€та, либо не существует");
                return false;
            }
        }

        return true;
    }

    private bool IsFreeCell((int i, int j) indexes)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int indexI = indexes.i + i;
                int indexJ = indexes.j + j;

                if (HaveCell(indexI, indexJ) && _cells[indexI, indexJ].Deck)
                {
                    Debug.Log($"¬ €чейку {_cells[indexes.i, indexes.j].transform.position} нельз€ установить корабль, " +
                        $"потому что €чейка {_cells[indexI, indexJ].transform.position} зан€та");
                    return false;
                }
            }
        }

        return true;
    }

    private bool HaveCell(int i, int j) => i >= 0 && j >= 0 && i < _size && j < _size;

    private (int, int) GetIndexes(CellConstructor cell)
    {
        for(int i = 0; i < _size; i++)
        {
            for(int j = 0; j < _size; j++)
            {
                if(cell == _cells[i, j])
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}
