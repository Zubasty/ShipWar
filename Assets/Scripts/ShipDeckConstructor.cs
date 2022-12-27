using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDeckConstructor : MonoBehaviour
{
    public ShipConstructor Ship { get; private set; }

    public void Initalization(ShipConstructor ship)
    {
        Ship = ship;
    }
}
