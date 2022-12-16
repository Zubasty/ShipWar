using System;
using UnityEngine;

public class ShipConstructor : MonoBehaviour
{
    public event Action Deinstalled;

    public void Deinstall()
    {
        Deinstalled?.Invoke();
    }
}
