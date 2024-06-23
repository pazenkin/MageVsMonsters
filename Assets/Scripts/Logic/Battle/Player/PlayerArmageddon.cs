using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Battle.Player
{
    /// <summary>
    /// Система атаки оружием игрока Армагеддон
    /// </summary>
    public class PlayerArmageddon : BasePlayerWeapon
    {
        public override PlayerWeaponType Type => PlayerWeaponType.Armageddon;

        protected override Vector3 GetProjectilePosition() => _player.transform.position;
    }
}