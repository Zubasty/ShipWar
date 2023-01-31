using UnityEngine;

[CreateAssetMenu(fileName = "ShipConstructorSettings", menuName = "ScriptableObjects/ShipConstructorSettings")]
public class ShipDeckConstructorSettings : ScriptableObject
{
    [SerializeField] private Color _isntSelected;
    [SerializeField] private Color _isSelected;
    [SerializeField] private Color _isSecondarySelected;

    public Color IsntSelected => _isntSelected;
    public Color IsSelected => _isSelected;
    public Color IsSecondarySelected => _isSecondarySelected;
}
