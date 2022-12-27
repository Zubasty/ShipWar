using UnityEngine;

public class CellConstructor : MonoBehaviour
{
    private ShipDeckConstructor _deck;

    public ShipDeckConstructor Deck => _deck;

    public void InstallDeck(ShipDeckConstructor deck)
    {
        _deck = deck;
        _deck.Ship.SetPosition(deck, transform.position);
        _deck.Ship.Deinstalled += OnDeinstalled;
    }

    private void OnDeinstalled()
    {
        _deck.Ship.Deinstalled -= OnDeinstalled;
        _deck.Ship.transform.position = new Vector3(-9, 0, 0);
        _deck = null;
    }
}
