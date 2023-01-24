using UnityEngine;

public class CellConstructor : MonoBehaviour
{
    private ShipDeckConstructor _deck;

    public ShipDeckConstructor Deck => _deck;

    public void InstallDeck(ShipDeckConstructor deck)
    {
        _deck = deck;
        _deck.Ship.Deinstalled += OnDeinstalled;
    }

    private void OnDeinstalled(ShipConstructor owner)
    {
        _deck.Ship.Deinstalled -= OnDeinstalled;
        _deck = null;
    }
}
