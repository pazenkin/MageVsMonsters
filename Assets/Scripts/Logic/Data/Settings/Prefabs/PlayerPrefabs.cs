using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Префабы моделей игроков по классам
    /// </summary>
    // [CreateAssetMenu(fileName = "PlayerPrefabs", menuName = "Settings/PlayerPrefabs", order = 0)]
    public class PlayerPrefabs : ScriptableObject
    {
        [SerializeField] private List<PlayerPrefabsElement> _elements;

        public AssetReference GetPrefabKey(PlayerType playerType)
        {
            return _elements.FirstOrDefault(x => x.Type == playerType)?.PrefabKey;
        }
    }

    [Serializable]
    public class PlayerPrefabsElement
    {
        public PlayerType Type;
        public AssetReference PrefabKey;
    }
}