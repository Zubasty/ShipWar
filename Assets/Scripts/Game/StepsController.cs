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
            _user.Attacked += OnAttackedUser;
            _enemy.Attacked += OnAttackedEnemy;
        }

        private void OnDisable()
        {
            _user.Attacked -= OnAttackedUser;
            _enemy.Attacked -= OnAttackedEnemy;
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

        private void OnAttackedUser(Cell cell)
        {
            if (_turnUser == false)
                Step(cell); 
        }

        private void OnAttackedEnemy(Cell cell)
        {
            if (_turnUser)
                Step(cell);
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
