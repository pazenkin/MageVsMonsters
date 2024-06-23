using UnityEngine;

namespace Logic.Levels
{
    /// <summary>
    /// Объект игрового уровня на сцене
    /// </summary>
    public class LevelZone : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;

        public Vector3 PlayerSpawnPoint => _playerSpawnPoint == null ? Vector3.zero : _playerSpawnPoint.position;
    }
}