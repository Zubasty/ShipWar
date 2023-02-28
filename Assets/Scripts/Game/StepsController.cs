using UnityEngine;

namespace Game
{
    public class StepsController : MonoBehaviour
    {
        [SerializeField] private User _user;
        [SerializeField] private Enemy _enemy;

        private bool _turnUser;

        private void OnEnable()
        {
            _user.TakingAttack += OnAttacked;
            _enemy.TakingAttack += OnAttacked;
        }

        private void OnDisable()
        {
            _user.TakingAttack -= OnAttacked;
            _enemy.TakingAttack -= OnAttacked;
        }

        private void Start()
        {
            _turnUser = Random.Range(0, 2) == 0;

            StepEnemy();
        }

        private void Step(Cell cell)
        {
            cell.Open();

            if (cell.HaveDeck == false)
            {
                _turnUser = !_turnUser;
            }

            StepEnemy();
        }

        private void OnAttacked(Cell cell, Player owner)
        {
            if(owner == _user)
            {
                if (_turnUser == false)
                    Step(cell);
            }
            else
            {
                if (_turnUser)
                    Step(cell);
            }
        }

        private void StepEnemy()
        {
            if (_turnUser == false)
            {
                _enemy.Step();
            }
        }
    }
}
