using UnityEngine;

public class CellConstructor : MonoBehaviour
{
    private ShipConstructor _ship;

    public void InstallShip(ShipConstructor ship)
    {
        _ship = ship;
        _ship.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        _ship.Deinstalled += OnDeinstalled;
    }

    public void OnDeinstalled()
    {
        _ship.Deinstalled -= OnDeinstalled;
        _ship.transform.position = new Vector3(-9, 0, 0);
        _ship = null;
    }
}
