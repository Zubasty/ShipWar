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

    public void Take(ShipConstructor ship, CellConstructor cell)
    {
        cell.InstallShip(ship);
    }
}
