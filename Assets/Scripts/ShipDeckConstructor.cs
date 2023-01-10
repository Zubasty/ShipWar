using UnityEngine;

public class ShipDeckConstructor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    public ShipConstructor Ship { get; private set; }

    public void Initalization(ShipConstructor ship)
    {
        Ship = ship;
    }

    public void OnVisual()
    {
        _renderer.enabled = true;
    }

    public void OffVisual()
    {
        _renderer.enabled = false;
    }
}
