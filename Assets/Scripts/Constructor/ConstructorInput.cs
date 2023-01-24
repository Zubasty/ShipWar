using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructorInput : MonoBehaviour
{
    [SerializeField] private MapConstructor _map;
    [SerializeField] private Button _buttonRotateShip;
    [SerializeField] private Button _buttonDropShip;

    //private ShipConstructor _ship;
    private ShipDeckConstructor _deck;

    private void OnEnable()
    {
        _buttonRotateShip.onClick.AddListener(RotateShip);
        _buttonDropShip.onClick.AddListener(DropShip);
    }

    private void OnDisable()
    {
        _buttonRotateShip.onClick.RemoveListener(RotateShip);
        _buttonDropShip.onClick.RemoveListener(DropShip);
    }

    private void Start()
    {
        SetActivityButtons();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider)
            {
                if(hit.collider.TryGetComponent(out ShipDeckConstructor deck))
                {
                    if (_deck)
                    {
                        DeselectShip();
                    }

                    _deck = deck;
                    _deck.Select();
                    SetActivityButtons();
                }
                if(hit.collider.TryGetComponent(out CellConstructor cell))
                {
                    if(_deck != null)
                    {           
                        if(_map.CanTakeDeck(_deck, cell))
                        {
                            _map.Take(_deck, cell);
                            Debug.Log($"Корабль {_deck.Ship.name} был установлен в {cell.transform.position}");
                        }
                        DeselectShip();
                        _deck = null;
                        SetActivityButtons();
                    }
                    else
                    {
                        Debug.LogError("Корабль не был выбран");
                    }
                }
            }
        }
    }

    private void SetActivityButtons()
    {
        if (_deck)
        {
            if (_deck.Ship.IsInstalled)
            {
                _buttonRotateShip.interactable = false;
                _buttonDropShip.interactable = true;
            }
            else
            {
                _buttonRotateShip.interactable = _deck.Ship.CountDecks > 1;
                _buttonDropShip.interactable = false;
            }
        }
        else
        {
            _buttonRotateShip.interactable = false;
            _buttonDropShip.interactable = false;
        }
    }

    private void DropShip()
    {
        if (_deck && _deck.Ship.IsInstalled)
        {
            Debug.Log($"Корабль {_deck.Ship.name} больше не выбран");
            _deck.Ship.Deinstall();
            DeselectShip();
            _deck = null;
            SetActivityButtons();
        }
    }

    private void RotateShip()
    {
        if (_deck && _deck.Ship.IsInstalled == false)
        {
            _deck.Ship.Rotate();
        }
    }

    private void DeselectShip()
    {
        for (int i = 0; i < _deck.Ship.CountDecks; i++)
        {
            _deck.Ship[i].Deselect();
        }
    }
}
