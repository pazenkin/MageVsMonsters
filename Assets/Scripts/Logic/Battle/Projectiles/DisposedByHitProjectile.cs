using UnityEngine;

namespace Logic.Battle.Projectiles
{
    /// <summary>
    /// Поведение снаряда, уничтожаемого при первом тригере с противником
    /// </summary>
    [RequireComponent(typeof(Projectile))]
    public class DisposedByHitProjectile : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private void OnEnable()
        {
            _projectile.OnSetDamage += Hit;
        }

        private void OnDisable()
        {
            _projectile.OnSetDamage -= Hit;
        }

        private void Hit()
        {
            _projectile.OnDispose?.Invoke();
        }
    }
}