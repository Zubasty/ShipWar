using UnityEngine;
using Game;

public class PositionVerticalAnimation : MonoBehaviour
{
    [SerializeField] private float _maxDelta;
    [SerializeField] private float _speed;
    [SerializeField] private CellVisual _cell;

    private float _maxError = 0.01f;
    private float _defaultY;
    private float _direction = 1;
    private bool _onAnimation;

    private void Start()
    {
        _defaultY = transform.position.y;
        _onAnimation = _cell.HaveDeck;
    }

    private void Update()
    {
        if (_onAnimation)
        {
            if (Mathf.Abs(_defaultY - transform.position.y) <= _maxDelta + _maxError)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + _speed * _direction * Time.deltaTime, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, _defaultY + _direction * _maxDelta, transform.position.z);
                _direction *= -1;
            }
        }
    }
}
