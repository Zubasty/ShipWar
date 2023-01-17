using UnityEngine;

public class ShipDeckConstructor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private ShipDeckConstructorSettings _settings;

    public ShipConstructor Ship { get; private set; }

    public void Initalization(ShipConstructor ship)
    {
        Ship = ship;
        _renderer.color = _settings.IsntSelected;
    }

    public void OnVisual()
    {
        _collider.enabled = _renderer.enabled = true;
    }

    public void OffVisual()
    {
        _collider.enabled = _renderer.enabled = false;
    }

    public void Select()
    {
        _renderer.color = _settings.IsSelected;

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
        _renderer.color = _settings.IsSecondarySelected;
    }

    public void Deselect()
    {
        _renderer.color = _settings.IsntSelected;
    }
}
