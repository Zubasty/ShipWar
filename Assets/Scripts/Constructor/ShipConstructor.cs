using System;
using UnityEngine;

public class ShipConstructor : MonoBehaviour
{
    private const int RotateValue = 270;
    private const int NotRotateValue = 0;

    [SerializeField] private ShipDeckConstructor[] _decks;

    private Vector3 _defaultPosition;

    public event Action<ShipConstructor> Deinstalled;
    public event Action<ShipConstructor> Installed;

    public bool IsRotated => transform.rotation.eulerAngles.z == RotateValue;

    public bool IsInstalled { get; private set; }

    public int CountDecks => _decks.Length;

    public ShipDeckConstructor this[int i]
    {
        get
        {
            return _decks[i];
        }
    }

    private void Start()
    {
        IsInstalled = false;
        _defaultPosition = transform.position;

        foreach (ShipDeckConstructor deck in _decks)
        {
            deck.Initalization(this);
        }
    }

    public int GetNumberDeck(ShipDeckConstructor deck)
    {
        for(int i = 0; i < _decks.Length; i++)
        {
            if(deck == _decks[i])
            {
                return i;
            }
        }
        return -1;
    }

    public void Install(ShipDeckConstructor deck, Vector3 position)
    {
        if (IsInstalled)
        {
            throw new Exception($"Попытка установить уже установленный корабль {name}");
        }

        SetPosition(deck, position);
        Installed?.Invoke(this);
        IsInstalled = true;
    }

    public void Rotate() 
    {
        if(IsInstalled)
        {
            throw new Exception($"Попытка повернуть уже установленный корабль {name}");
        }

        transform.rotation = Quaternion.Euler(0, 0, IsRotated ? NotRotateValue : RotateValue);
    }

    public void Deinstall()
    {
        if (IsInstalled == false)
        {
            throw new Exception($"Попытка сбросить еще неустановленный корабль {name}");
        }

        transform.position = _defaultPosition;
        Deinstalled?.Invoke(this);
        IsInstalled = false;       
    }

    public void OnVisual()
    {
        foreach(ShipDeckConstructor deck in _decks)
        {
            deck.OnVisual();
        }
    }

    public void OffVisual()
    {
        foreach (ShipDeckConstructor deck in _decks)
        {
            deck.OffVisual();
        }
    }

    private void SetPosition(ShipDeckConstructor deck, Vector3 position)
    {
        int i = GetNumberDeck(deck);
        float mid = (CountDecks - 1) / 2.0f;
        float shift = mid - i;

        if (IsRotated)
        {
            transform.position = position + new Vector3(shift, 0, -1);
        }
        else
        {
            transform.position = position + new Vector3(0, shift, -1);
        }
    }
}
