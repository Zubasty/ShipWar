using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropperShips : MonoBehaviour
{
    [SerializeField] private ShipConstructor[] _ships;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(DropShips);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(DropShips);
    }

    private void DropShips()
    {
        foreach (ShipConstructor ship in _ships)
            if(ship.IsInstalled)
                ship.Deinstall();
    }
}
