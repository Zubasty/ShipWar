using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QueueShipsConstructor : MonoBehaviour
{
    [SerializeField] private ShipConstructor[] _ships;

    private Queue<ShipConstructor>[] _queues;
    private int _countTypeShips = 4;

    private void Awake()
    {
        _queues = new Queue<ShipConstructor>[_countTypeShips];

        for (int i = 0; i < _countTypeShips; i++)
        {
            _queues[i] = new Queue<ShipConstructor>();
        }

        foreach (ShipConstructor ship in _ships)
        {
            _queues[ship.CountDecks - 1].Enqueue(ship);
        }
    }

    private void OnEnable()
    {
        for(int i = 0; i < _queues.Length; i++)
        {
            foreach(ShipConstructor ship in _queues[i])
            {
                ship.Installed += OnInstalled;
                ship.Deinstalled += OnDeinstalled;
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _queues.Length; i++)
        {
            foreach (ShipConstructor ship in _queues[i])
            {
                ship.Installed -= OnInstalled;
                ship.Deinstalled -= OnDeinstalled;
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < _queues.Length; i++)
        {
            foreach (ShipConstructor ship in _queues[i])
            {
                ship.OffVisual();
            }
        }

        foreach (var queue in _queues)
        {
            queue.Peek().OnVisual();
        }
    }

    private void OnInstalled(ShipConstructor ship)
    {
        int number = ship.CountDecks - 1;

        _queues[number].Dequeue();

        if (_queues[number].Count > 0)
        {
            _queues[number].Peek().OnVisual();
        }
    }

    private void OnDeinstalled(ShipConstructor ship)
    {
        _queues[ship.CountDecks - 1].Enqueue(ship);

        if (_queues[ship.CountDecks - 1].Count > 1)
        {
            ship.OffVisual();
        }
    }
}
