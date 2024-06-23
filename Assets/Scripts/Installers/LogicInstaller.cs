using Logic.Battle.Player;
using Logic.Characters;
using Logic.Core;
using Logic.Enemies;
using Logic.Levels;
using UnityEngine;
using Zenject;

namespace Installers
{
    /// <summary>
    /// Инсталлер Zenject для работы основной логики CoreGameplay
    /// </summary>
    public class LogicInstaller : MonoInstaller
    {
        [SerializeField] private CoreGameplayMain _coreGameplayMain;
        [SerializeField] private Player _player;
        [SerializeField] private NavMeshGenerator _navMeshGenerator;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_coreGameplayMain);
            Container.Bind<PauseState>().AsSingle().Lazy();
            Container.Bind<Level>().AsSingle().Lazy();

            Container.BindInstance(_navMeshGenerator);

            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().Lazy();
            Container.Bind<EnemyFactory>().AsSingle().Lazy();

            Container.BindInstance(_player);
            Container.BindInterfacesAndSelfTo<PlayerWeapon>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<PlayerAttack>().AsSingle().NonLazy();
        }
    }
}