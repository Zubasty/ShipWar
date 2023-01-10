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
                            Debug.Log($"������� {_deck.Ship.name} ��� ���������� � {cell.transform.position}");
                        }
                        _deck = null;
                    }
                    else
                    {
                        Debug.LogError("������� �� ��� ������");
                    }
                }
            }
        }
    }

    public void DropShip()
    {
        if (_deck)
        {
            Debug.Log($"������� {_deck.Ship.name} ������ �� ������");
            _deck.Ship.Deinstall();
            _deck = null;
        }
    }

    public void RotateShip()
    {
        if (_deck)
        {
            _deck.Ship.Rotate();
        }
    }
}
