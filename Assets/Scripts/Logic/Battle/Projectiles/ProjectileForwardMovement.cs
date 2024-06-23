using UnityEngine;

namespace Logic.Battle.Projectiles
{
    /// <summary>
    /// Класс перемещения снаряда по прямой
    /// </summary>
    public class ProjectileForwardMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}