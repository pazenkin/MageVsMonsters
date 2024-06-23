using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Настройки генерации врагов
    /// </summary>
    // [CreateAssetMenu(fileName = "EnemySpawningSettings", menuName = "Settings/EnemySpawningSettings", order = 0)]
    public class EnemySpawningSettings : ScriptableObject
    {
        [SerializeField] private int _maxNumber;
        [SerializeField] private float _distanceFromPlayerForSpawning;
        [SerializeField] private List<EnemySpawningSettingsElement> _availableEnemies;
        
        public int MaxNumber => _maxNumber;
        public float DistanceFromPlayerForSpawning => _distanceFromPlayerForSpawning;
        
        public List<EnemyType> GetAvailableEnemies(LevelType levelType)
        {
            return _availableEnemies.FirstOrDefault(x => x.LevelType == levelType)?.AvailableEnemies;
        }
    }
    
    [Serializable]
    public class EnemySpawningSettingsElement
    {
        public LevelType LevelType;
        public List<EnemyType> AvailableEnemies;
    }
}