using Logic.Characters;
using Logic.Data.Settings;
using Zenject;

namespace Installers
{
    /// <summary>
    /// Инсталлер Zenject для пулов объектов
    /// </summary>
    public class PoolsInstaller : MonoInstaller
    {
        [Inject] private readonly PoolPrefabs _poolPrefabs;
        
        public override void InstallBindings()
        {
            Container.BindFactory<MainCharacterData, InjectableCharacter, PlayerCharacterFactory>()
                .FromComponentInNewPrefab(_poolPrefabs.Player);
            
            Container.BindMemoryPool<Character, EnemyPool>()
                .WithInitialSize(1)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_poolPrefabs.Enemy);
        }
    }
}