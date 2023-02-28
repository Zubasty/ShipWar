using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private User _user;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _textEndGame;
        [SerializeField] private Button _buttonToConstructor;
        [SerializeField] private int _numberSceneConstructor;

        private void OnEnable()
        {
            _user.TookAttack += OnTookAttack;
            _enemy.TookAttack += OnTookAttack;
            _buttonToConstructor.onClick.AddListener(OnToConstructor);
        }

        private void OnDisable()
        {
            _user.TookAttack -= OnTookAttack;
            _enemy.TookAttack -= OnTookAttack;
            _buttonToConstructor.onClick.RemoveListener(OnToConstructor);
        }
        
        private void OnTookAttack(Cell cell, Player player)
        {
            if (player.IsLose)
            {
                _panel.SetActive(true);

                if(player == _user)
                {
                    _textEndGame.text = "Ты проиграл";
                }
                else
                {
                    _textEndGame.text = "Ты выиграл";
                }
            }
        }

        private void OnToConstructor()
        {
            SceneManager.LoadScene(_numberSceneConstructor);
        }
    }
}
