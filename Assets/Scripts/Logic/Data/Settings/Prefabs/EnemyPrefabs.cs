using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Префабы моделей врагов
    /// </summary>
    // [CreateAssetMenu(fileName = "EnemyPrefabs", menuName = "Settings/EnemyPrefabs", order = 0)]
    public class EnemyPrefabs : ScriptableObject
    {
        [SerializeField] private List<EnemyPrefabsElement> _elements;
        
        private Dictionary<EnemyType, AssetReference> _hashedElements;

        private void OnEnable()
        {
            _hashedElements = _elements.ToDictionary(x => x.Type, x => x.PrefabKey);
        }
        
        public AssetReference GetPrefabKey(EnemyType enemyType)
        {
            return _hashedElements[enemyType];
        }
    }

    [Serializable]
    public class EnemyPrefabsElement
    {
        public EnemyType Type;
        public AssetReference PrefabKey;
    }
}