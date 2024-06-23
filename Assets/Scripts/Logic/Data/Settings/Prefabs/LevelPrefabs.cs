using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Список доступных префабов уровней
    /// </summary>
    // [CreateAssetMenu(fileName = "LevelPrefabs", menuName = "Settings/LevelPrefabs", order = 0)]
    public class LevelPrefabs : ScriptableObject
    {
        [SerializeField] private List<LevelPrefabsElement> _elements;

        public AssetReference GetPrefabKey(LevelType levelType)
        {
            return _elements.FirstOrDefault(x => x.Type == levelType)?.PrefabKey;
        }
    }

    [Serializable]
    public class LevelPrefabsElement
    {
        public LevelType Type;
        public AssetReference PrefabKey;
    }
}