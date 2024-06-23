using System;
using System.Collections.Generic;
using System.Linq;
using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Настройки оружия игрока
    /// </summary>
    // [CreateAssetMenu(fileName = "PlayerWeaponSettings", menuName = "Settings/PlayerWeaponSettings", order = 0)]
    public class PlayerWeaponSettings : ScriptableObject
    {
        [SerializeField] private List<PlayerAvailableWeaponElement> _availableWeapons;
        [SerializeField] private List<PlayerWeaponSettingsElement> _elements;

        private Dictionary<PlayerType, HashSet<PlayerWeaponType>> _hashedAvailableWeapons;
        private Dictionary<PlayerWeaponType, PlayerWeaponSettingsElement> _hashedElements;

        private void OnEnable()
        {
            _hashedAvailableWeapons = new();
            foreach (var item in _availableWeapons)
            {
                var hashedElements = new HashSet<PlayerWeaponType>();
                foreach (var playerWeaponType in item.WeaponType)
                {
                    hashedElements.Add(playerWeaponType);
                }
                _hashedAvailableWeapons.Add(item.PlayerType, hashedElements);
            }

            _hashedElements = _elements.ToDictionary(x => x.Type, x => x);
        }

        public bool WeaponIsAvailable(PlayerType playerType, PlayerWeaponType playerWeaponType)
        {
            return _hashedAvailableWeapons[playerType].Contains(playerWeaponType);
        }

        public PlayerWeaponSettingsElement GetData(PlayerWeaponType playerWeaponType)
        {
            return _hashedElements[playerWeaponType];
        }
    }

    [Serializable]
    public struct PlayerAvailableWeaponElement
    {
        public PlayerType PlayerType;
        public List<PlayerWeaponType> WeaponType;
    }

    [Serializable]
    public struct PlayerWeaponSettingsElement
    {
        public PlayerWeaponType Type;
        public WeaponData WeaponData;
    }
}