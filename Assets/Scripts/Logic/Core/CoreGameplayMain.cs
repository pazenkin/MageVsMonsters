using System.Collections;
using Cysharp.Threading.Tasks;
using Logic.Battle.Player;
using Logic.Characters;
using Logic.Enemies;
using Logic.Levels;
using Logic.Signals;
using UniRx;
using UnityEngine;
using Utilities;
using Zenject;

namespace Logic.Core
{
    /// <summary>
    /// Управляющий класс CoreGameplay
    /// </summary>
    public class CoreGameplayMain : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly PauseState _pauseState;
        [Inject] private readonly AddressablesHelper _addressablesHelper;
        [Inject] private readonly Level _level;
        [Inject] private readonly NavMeshGenerator _navMeshGenerator;
        [Inject] private readonly Player _player;
        [Inject] private readonly EnemySpawner _enemySpawner;
        [Inject] private readonly PlayerWeapon _playerWeapon;
        
        private void Awake()
        {
            Observable
                .FromCoroutine(LoadingLevel)
                .SelectMany(LoadingNavMeshGenerator)
                .SelectMany(LoadingPlayer)
                .SelectMany(LoadingPlayerWeapons)
                .SelectMany(LoadingEnemySpawner)
                .Subscribe(_ => OnCompleteLoading())
                .AddTo(this);
        }

        private void OnCompleteLoading()
        {
            _pauseState.Value = false;
            _signalBus.Fire<LevelLoadedEvent>();
        }

        private void OnDestroy()
        {
            _pauseState.Value = true;
            _addressablesHelper.DisposeAll();
        }
        
        private IEnumerator LoadingLevel() => _level.Initialize().ToCoroutine();
        private IEnumerator LoadingNavMeshGenerator() => _navMeshGenerator.Initialize().ToCoroutine();
        private IEnumerator LoadingPlayer() => _player.Initialize().ToCoroutine();
        private IEnumerator LoadingPlayerWeapons() => _playerWeapon.Initialize().ToCoroutine();
        private IEnumerator LoadingEnemySpawner() => _enemySpawner.Initialize().ToCoroutine();
    }
}