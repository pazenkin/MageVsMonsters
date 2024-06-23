using System;
using UnityEngine;

namespace Logic.Data.Settings
{
    /// <summary>
    /// Основные данные персонажа
    /// </summary>
    [Serializable]
    public struct MainCharacterData
    {
        [Min(0f)] public float Health;
        [Range(0f, 1f)] public float Armor;
        [Min(0f)] public float Speed;
    }
}