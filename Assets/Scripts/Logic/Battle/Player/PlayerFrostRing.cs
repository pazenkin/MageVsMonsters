using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Battle.Player
{
    /// <summary>
    /// Система атаки оружием игрока Ледяное кольцо
    /// </summary>
    public class PlayerFrostRing : BasePlayerWeapon
    {
        public override PlayerWeaponType Type => PlayerWeaponType.FrostRing;
        
        private const float DISTANCE_FROM_PLAYER = 5f;

        protected override Vector3 GetProjectilePosition() =>
            _player.transform.position + _player.Character.transform.forward * DISTANCE_FROM_PLAYER;
    }
}