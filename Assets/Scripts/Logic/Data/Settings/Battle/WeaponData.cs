using System;
using TriInspector;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Параметры оружия
    /// </summary>
    [Serializable]
    public struct WeaponData
    {
        [Min(0f)] public float Damage;
        [InfoBox("Используется только у игрока")] [Min(0f)] public float ReloadTime;
    }
}