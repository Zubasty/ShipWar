using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorInput : MonoBehaviour
{
    [SerializeField] private Map _map;

    //private ShipConstructor _ship;
    private ShipDeckConstructor _deck;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider)
            {
                if(hit.collider.TryGetComponent(out ShipDeckConstructor deck))
                {
                    Debug.Log(deck.Ship);
                    _deck = deck;
                }
                if(hit.collider.TryGetComponent(out CellConstructor cell))
                {
                    if(_deck != null)
                    {           
                        if(_map.Take(_deck, cell))
                        {
                            Debug.Log($"Корабль {_deck.Ship.name} был установлен в {cell.transform.position}");
                        }
                        _deck = null;
                    }
                    else
                    {
                        Debug.LogError("Корабль не был выбран");
                    }
                }
            }
            else
            {
                if (_deck)
                {
                    Debug.Log($"Корабль {_deck.Ship.name} был убран из ячейки");
                    _deck.Ship.Deinstall();
                    _deck = null;
                } 
            }
        }
    }
}
