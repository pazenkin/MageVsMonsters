using System;
using System.Collections.Generic;
using CustomDynamicEnums;
using UnityEngine;

namespace Ui.Settings
{
    /// <summary>
    /// Спрайты скилов игрока
    /// </summary>
    // [CreateAssetMenu(fileName = "PlayerSkillSprites", menuName = "Settings/PlayerSkillSprites", order = 0)]
    public class PlayerSkillSprites : ScriptableObject
    {
        [SerializeField] private List<PlayerSkillSpritesElement> _elements;
        
        private readonly Dictionary<PlayerWeaponType, Sprite> _sprites = new();
        
        public Sprite GetSprite(PlayerWeaponType type)
        {
            if (!_sprites.ContainsKey(type))
            {
                _sprites.Add(type, _elements.Find(x => x.PlayerWeaponType == type).Sprite);
            }
            return _sprites[type];
        }
    }
    
    [Serializable]
    public class PlayerSkillSpritesElement
    {
        public PlayerWeaponType PlayerWeaponType;
        public Sprite Sprite;
    }
}