using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Настройки доступных для игрока персонажей
    /// </summary>
    // [CreateAssetMenu(fileName = "PlayersSettings", menuName = "Settings/PlayersSettings", order = 0)]
    public class PlayersSettings : ScriptableObject
    {
        [SerializeField] private List<PlayerSettingsElement> _elements;
        
        public MainCharacterData GetPlayerData(PlayerType type)
        {
            return _elements.First(x => x.Type == type).Data;
        }
    }
    
    [Serializable]
    public class PlayerSettingsElement
    {
        public PlayerType Type;
        public MainCharacterData Data;
    }
}