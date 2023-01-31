using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSaverConstructor : MonoBehaviour
{
    [SerializeField] private MapsSO _maps;
    [SerializeField] private MapConstructor _map;
    [SerializeField] private ConstructorRandom _random;
    [SerializeField] private DropperShips _dropper;
    [SerializeField] private int _numberGameScene;
    [SerializeField] private GameObject _panelLoading;

    public void Save()
    {
        _panelLoading.SetActive(true);
        _maps.TakeMattrixPlayer(_map.GetMattrixMap());
        _dropper.DropShips();
        _random.Random();
        _maps.TakeMattrixEnemy(_map.GetMattrixMap());
        _maps.ShowConsole();
        SceneManager.LoadScene(_numberGameScene);
    }
}
