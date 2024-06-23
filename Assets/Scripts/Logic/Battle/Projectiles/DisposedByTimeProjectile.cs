using UnityEngine;

namespace Logic.Battle.Projectiles
{
    /// <summary>
    /// Поведение снаряда, уничтожаемого по времени после создания
    /// </summary>
    [RequireComponent(typeof(Projectile))]
    public class DisposedByTimeProjectile : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _timeToDispose;

        private float _timer;
        private bool _isActive;

        private void OnEnable()
        {
            _isActive = true;
            _timer = _timeToDispose;
        }

        private void Update()
        {
            if (!_isActive) return;

            _timer -= Time.deltaTime;
            if (_timer > 0f) return;

            _isActive = false;
            _projectile.OnDispose?.Invoke();
        }
    }
}