using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructorRandom : MonoBehaviour
{
    [SerializeField] private MapConstructor _map;
    [SerializeField] private ShipConstructor[] _ships;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonUIClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonUIClick);
    }

    private void OnButtonUIClick()
    {
        foreach(ShipConstructor ship in _ships)
        {
            if(ship.IsInstalled == false)
            {
                if(UnityEngine.Random.value >= 0.5f)
                    ship.Rotate();

                _map.Take(ship[UnityEngine.Random.Range(0, ship.CountDecks)]);
            }
        }
    }
}
