using Logic.Data.Settings;
using Zenject;

namespace Logic.Characters
{
    /// <summary>
    /// Фабрика по генерации персонажа игрока
    /// </summary>
    public class PlayerCharacterFactory : PlaceholderFactory<MainCharacterData, InjectableCharacter> { }
}