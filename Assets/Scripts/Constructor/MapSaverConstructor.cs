using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSaverConstructor : MonoBehaviour
{
    [SerializeField] private ShipConstructor[] _ships;
    [SerializeField] private MapsSO _maps;
    [SerializeField] private MapConstructor _map;
    [SerializeField] private ConstructorRandom _random;
    [SerializeField] private DropperShips _dropper;
    [SerializeField] private int _numberGameScene;
    [SerializeField] private GameObject _panelLoading;
    [SerializeField] private Button _startGameButton;

    private bool HaveFreeShip
    {
        get
        {
            for (int i = 0; i < _ships.Length; i++)
            {
                if (_ships[i].IsInstalled == false)
                {
                    return true;
                }
            }

            return false;
        }
    }

    private void OnEnable()
    {
        for(int i = 0; i < _ships.Length; i++)
        {
            _ships[i].Installed += SetActiveButton;
            _ships[i].Deinstalled += SetActiveButton;
        }

        _startGameButton.onClick.AddListener(Save);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _ships.Length; i++)
        {
            _ships[i].Installed -= SetActiveButton;
            _ships[i].Deinstalled -= SetActiveButton;
        }

        _startGameButton.onClick.RemoveListener(Save);
    }

    private void Save()
    {
        _panelLoading.SetActive(true);
        _maps.TakeMattrixPlayer(_map.GetMattrixMap());
        _dropper.DropShips();
        _random.Random();
        _maps.TakeMattrixEnemy(_map.GetMattrixMap());
        _maps.ShowConsole();
        SceneManager.LoadScene(_numberGameScene);
    }

    private void SetActiveButton(ShipConstructor ship)
    {
        Debug.Log(HaveFreeShip);
        _startGameButton.interactable = HaveFreeShip == false;
    }
}
