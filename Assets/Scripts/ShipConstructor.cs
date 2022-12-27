using System;
using UnityEngine;

public class ShipConstructor : MonoBehaviour
{
    private const int RotateValue = -90;
    private const int NotRotateValue = 0;

    [SerializeField] private ShipDeckConstructor[] _decks;

    public event Action Deinstalled;

    public bool IsRotated => transform.rotation.eulerAngles.z == RotateValue;

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
        foreach(ShipDeckConstructor deck in _decks)
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

    public void SetPosition(ShipDeckConstructor deck, Vector3 position)
    {
        int i = GetNumberDeck(deck);

        if (CountDecks % 2 == 0)
        {

        }
        else
        {
            int mid = (CountDecks - 1) / 2;
            transform.position = position + new Vector3(i - mid, 0, 0);
        }
    }

    public void Rotate() => transform.rotation = Quaternion.Euler(0, 0, IsRotated ? NotRotateValue : RotateValue);

    public void Deinstall()
    {
        Deinstalled?.Invoke();
    }
}
