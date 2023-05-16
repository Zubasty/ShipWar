using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [SerializeField] private float _startPositionX;
    [SerializeField] private float _endPositionX;
    [SerializeField] private float _speed;

    void Update()
    {
        transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x >= _endPositionX)
        {
            transform.position = new Vector3(_startPositionX, transform.position.y, transform.position.z);
        }
    }
}
