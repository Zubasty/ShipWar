using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
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

    public bool Take(ShipDeckConstructor deck, CellConstructor cell)
    {
        bool goToRight = deck.Ship.IsRotated;
        int countDecks = deck.Ship.CountDecks;
        int numberDeck = deck.Ship.GetNumberDeck(deck);

        for(int i = 0; i < countDecks; i++)
        {
            if (goToRight)
            {
                (int i, int j) indexes = GetIndexes(cell);
                indexes.i -= numberDeck + i;
                if ((HaveCell(indexes.i, indexes.j) && IsFreeCell(indexes)) == false)
                {
                    Debug.Log($"������� ������ ���������� � ������ {cell.transform.position}, ������ ��� ��� ������, ���� �� ����������");
                    return false;
                }
            }
            else
            {

            }
        }  

        for (int i = 0; i < countDecks; i++)
        {
            if (goToRight)
            {
                (int i, int j) indexes = GetIndexes(cell);
                indexes.i -= numberDeck + i;
                _cells[indexes.i, indexes.j].InstallDeck(deck.Ship[i]);
            }
            else
            {

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
                    Debug.Log($"� ������ {_cells[indexes.i, indexes.j].transform.position} ������ ���������� �������, " +
                        $"������ ��� ������ {_cells[indexI, indexJ].transform.position} ������");
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
