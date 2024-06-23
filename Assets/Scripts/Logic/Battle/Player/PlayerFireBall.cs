using CustomDynamicEnums;
using UnityEngine;

namespace Logic.Battle.Player
{
    /// <summary>
    /// Система атаки оружием игрока Огненный шар
    /// </summary>
    public class PlayerFireBall : BasePlayerWeapon
    {
        public override PlayerWeaponType Type => PlayerWeaponType.FireBall;

        protected override Vector3 GetProjectilePosition() => _player.WeaponPoint.position;
        protected override Quaternion GetProjectileRotation() => _player.Character.transform.rotation;
    }
}