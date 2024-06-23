using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Префабы снарядов
    /// </summary>
    // [CreateAssetMenu(fileName = "ProjectilePrefabs", menuName = "Settings/ProjectilePrefabs", order = 0)]
    public class ProjectilePrefabs : ScriptableObject
    {
        [SerializeField] private List<ProjectilePrefabsElement> _elements;
        
        private Dictionary<PlayerWeaponType, AssetReference> _hashedElements;
        
        private void OnEnable()
        {
            _hashedElements = _elements.ToDictionary(x => x.Type, x => x.PrefabKey);
        }
        
        public AssetReference GetPrefabKey(PlayerWeaponType playerWeaponType)
        {
            return _hashedElements[playerWeaponType];
        }
    }
    
    [Serializable]
    public class ProjectilePrefabsElement
    {
        public PlayerWeaponType Type;
        public AssetReference PrefabKey;
    }
}