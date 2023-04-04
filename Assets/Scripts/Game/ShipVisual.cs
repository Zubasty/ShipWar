using UnityEngine;

namespace Game
{
    public class ShipVisual : MonoBehaviour
    {
        private const float ValueRotate = -90;

        [SerializeField] private SpriteRenderer _renderer;

        private Ship _ship;

        private void Start()
        {
            if(_ship.IsRotated)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + ValueRotate);
        }

        private void OnDestroy()
        {
            _ship.Destroyed -= OnDestroyed;
        }

        public void Init(Ship ship)
        {
            _ship = ship;
            _ship.Destroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _renderer.sprite = null;
        }
    }
}

