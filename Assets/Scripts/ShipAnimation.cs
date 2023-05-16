using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IRotater))]
public class ShipAnimation : MonoBehaviour
{
    [SerializeField] private float _maxDelta;
    [SerializeField] private float _speed;

    private IRotater _ship;
    private float _direction = 1;

    private void Awake()
    {
        _ship = GetComponent<IRotater>();
    }

    private void Update()
    {
        float angle = transform.rotation.eulerAngles.z;
        angle = angle > 180 ? angle - 360 : angle;
        
        if (Mathf.Abs(Mathf.DeltaAngle(angle, _ship.DefaultRotateValue)) <= _maxDelta)
        {
            transform.Rotate(new Vector3(0, 0, _direction * _speed * Time.deltaTime));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, _ship.DefaultRotateValue + _maxDelta * _direction);
            _direction *= -1;        
        }
    }
}
