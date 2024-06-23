using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Enemies
{
    /// <summary>
    /// Инфромация о модели врага
    /// </summary>
    public class EnemyModel : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        
        public EnemyType Type => _enemyType;
    }
}