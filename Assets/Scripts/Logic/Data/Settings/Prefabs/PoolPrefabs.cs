using Logic.Characters;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Префабы базовых объектов для пулов
    /// </summary>
    // [CreateAssetMenu(fileName = "PoolPrefabs", menuName = "Settings/PoolPrefabs", order = 0)]
    public class PoolPrefabs : ScriptableObject
    {
        [SerializeField] private InjectableCharacter _basePlayer;
        [SerializeField] private Character _baseEnemy;

        public InjectableCharacter Player => _basePlayer;
        public Character Enemy => _baseEnemy;
    }
}