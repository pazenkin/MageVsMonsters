using System;
using Cysharp.Threading.Tasks;
using Logic.Characters;
using Logic.Data.Settings;
using Logic.Levels;
using Logic.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Logic.Enemies
{
    /// <summary>
    /// Система, контроллирующая спавн новых врагов
    /// </summary>
    public class EnemySpawner : IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly EnemyFactory _enemyFactory;
        [Inject] private readonly Player _player;
        [Inject] private readonly Level _level;
        [Inject] private readonly EnemySpawningSettings _enemySpawningSettings;

        private int _enemiesNumber;
        
        public async UniTask Initialize()
        {
            _signalBus.Subscribe<LevelLoadedEvent>(GenerateStartingEnemies);
            _signalBus.Subscribe<CharacterDiedEvent>(ReturnCharacterToPool);
            
            await _enemyFactory.Initialize(_enemySpawningSettings.GetAvailableEnemies(_level.Type));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LevelLoadedEvent>(GenerateStartingEnemies);
            _signalBus.Unsubscribe<CharacterDiedEvent>(ReturnCharacterToPool);
        }

        private void GenerateStartingEnemies()
        {
            GenerateStartingEnemiesTask().Forget();
        }

        private async UniTask GenerateStartingEnemiesTask()
        {
            for (var i = 0; i < _enemySpawningSettings.MaxNumber; i++)
            {
                SpawnRandomEnemy();
                await UniTask.Yield();
            }
        }

        private void ReturnCharacterToPool(CharacterDiedEvent args)
        {
            if (_enemyFactory.Despawn(args.Character))
            {
                _enemiesNumber--;
            }
            SpawnRandomEnemy();
        }

        private void SpawnRandomEnemy()
        {
            if (_enemiesNumber >= _enemySpawningSettings.MaxNumber) return;
            
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var newEnemyPosition = new Vector3(
                _enemySpawningSettings.DistanceFromPlayerForSpawning * Mathf.Cos(angle) + _player.transform.position.x,
                0f,
                _enemySpawningSettings.DistanceFromPlayerForSpawning * Mathf.Sin(angle) + _player.transform.position.z);
            _enemyFactory.Spawn(newEnemyPosition);
            _enemiesNumber++;
        }
    }
}