using UnityEngine;

public class ShipDeckConstructor : MonoBehaviour
{
    [SerializeField] private SpriteMask _mask;
    [SerializeField] private Collider2D _collider;

    public ShipConstructor Ship { get; private set; }

    public void Initalization(ShipConstructor ship)
    {
        Ship = ship;
    }

    public void OnVisual()
    {
        _collider.enabled = true;
    }

    public void OffVisual()
    {
        _collider.enabled = false;
    }

    public void Select()
    {
        Ship.Select();
        _mask.enabled = true;

        for (int i = 0; i < Ship.CountDecks; i++)
        {
            if (Ship[i] != this)
            {
                Ship[i].SecondarySelect();
            }
        }
    }

    public void SecondarySelect()
    {
        _mask.enabled = false;
    }

    public void Deselect()
    {
        _mask.enabled = false;
    }
}
