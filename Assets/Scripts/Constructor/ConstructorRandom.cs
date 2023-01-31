using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructorRandom : MonoBehaviour
{
    [SerializeField] private MapConstructor _map;
    [SerializeField] private ShipConstructor[] _ships;
    [SerializeField] private Button _button;
    [SerializeField, Range(0,1)] private float _chanceRotate;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonUIClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonUIClick);
    }

    public void Random()
    {
        foreach (ShipConstructor ship in _ships)
        {
            if (ship.IsInstalled == false)
            {
                if (UnityEngine.Random.value >= _chanceRotate)
                    ship.Rotate();

                _map.Take(ship[UnityEngine.Random.Range(0, ship.CountDecks)]);
            }
        }
    }

    private void OnButtonUIClick()
    {
        Random();
    }
}
