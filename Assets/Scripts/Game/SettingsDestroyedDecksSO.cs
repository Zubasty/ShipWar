using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SettingsDestroyed", menuName = "ScriptableObjects/SettingsDestroyed")]
    public class SettingsDestroyedDecksSO : ScriptableObject
    {
        [SerializeField] private Sprite _spriteDestroyed;
        [SerializeField] private float _rotation;
        [SerializeField] private bool _isMirrored;

        public Sprite SpriteDestroyed => _spriteDestroyed;
        public float Rotation => _rotation;
        public bool IsMirrored => _isMirrored;
    }
}

