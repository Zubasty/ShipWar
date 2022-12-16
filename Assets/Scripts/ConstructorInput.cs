using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorInput : MonoBehaviour
{
    [SerializeField] private Map _map;
    
    private ShipConstructor _ship;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider)
            {
                if(hit.collider.TryGetComponent(out ShipConstructor ship))
                {
                    _ship = ship;
                }
                if(hit.collider.TryGetComponent(out CellConstructor cell))
                {
                    if(_ship != null)
                    {
                        Debug.Log($"Корабль {_ship.name} был установлен в {cell.transform.position}");
                        _map.Take(_ship, cell);
                        _ship = null;
                    }
                    else
                    {
                        Debug.LogError("Корабль не был выбран");
                    }
                }
            }
            else
            {
                if (_ship)
                {
                    Debug.Log($"Корабль {_ship.name} был убран из ячейки");
                    _ship.Deinstall();
                    _ship = null;
                } 
            }
        }
    }
}
