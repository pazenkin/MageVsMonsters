using System.Collections.Generic;

namespace Logic.Battle.Player
{
    /// <summary>
    /// Список классов для всех доступных в игре типов оружия игрока
    /// </summary>
    public class AllWeapons
    {
        public readonly List<BasePlayerWeapon> Values = new()
        {
            new PlayerFireBall(),
            new PlayerFrostRing(),
            new PlayerArmageddon()
        };
    }
}