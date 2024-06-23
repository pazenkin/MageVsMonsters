using Logic.Data.Settings;
using Logic.Services.Dictionaries;
using Zenject;

namespace Logic.Characters
{
    /// <summary>
    /// Пул по созданию персонажей врагов
    /// </summary>
    public class EnemyPool : MonoMemoryPool<MainCharacterData, Character>
    {
        [Inject] private readonly CharactersDictionary _charactersDictionary;

        protected override void Reinitialize(MainCharacterData mainCharacterData, Character item)
        {
            item.Initialize(mainCharacterData);
        }

        protected override void OnSpawned(Character item)
        {
            _charactersDictionary.Add(item);
        }

        protected override void OnDespawned(Character item)
        {
            _charactersDictionary.Remove(item);
        }
    }
}