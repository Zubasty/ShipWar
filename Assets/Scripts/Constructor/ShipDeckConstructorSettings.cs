using UnityEngine;

[CreateAssetMenu(fileName = "ShipConstructorSettings", menuName = "ScriptableObjects/ShipConstructorSettings")]
public class ShipDeckConstructorSettings : ScriptableObject
{
    [SerializeField] private Color _isntSelected;
    [SerializeField] private Color _isSelected;

    public Color IsntSelected => _isntSelected;
    public Color IsSelected => _isSelected;
}
