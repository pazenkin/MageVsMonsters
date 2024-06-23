using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Настройки врагов
    /// </summary>
    // [CreateAssetMenu(fileName = "EnemiesSettings", menuName = "Settings/EnemiesSettings", order = 0)]
    public class EnemiesSettings : ScriptableObject
    {
        [SerializeField] private List<EnemiesSettingsElement> _elements;
        
        private Dictionary<EnemyType, MainCharacterData> _hashedElements;
        private Dictionary<EnemyType, WeaponData> _hashedWeaponData;

        private void OnEnable()
        {
            _hashedElements = _elements.ToDictionary(x => x.Type, x => x.Data);
            _hashedWeaponData = _elements.ToDictionary(x => x.Type, x => x.WeaponData);
        }
        
        public MainCharacterData GetData(EnemyType enemyType)
        {
            return _hashedElements[enemyType];
        }
        
        public WeaponData GetWeaponData(EnemyType enemyType)
        {
            return _hashedWeaponData[enemyType];
        }
    }
    
    [Serializable]
    public class EnemiesSettingsElement
    {
        public EnemyType Type;
        public MainCharacterData Data;
        public WeaponData WeaponData;
    }
}