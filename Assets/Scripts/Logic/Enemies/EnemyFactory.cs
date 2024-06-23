using System.Collections.Generic;
using CustomDynamicEnums;
using Cysharp.Threading.Tasks;
using Logic.Characters;
using Logic.Data.Settings;
using Logic.Services.Dictionaries;
using UnityEngine;
using Utilities;
using Zenject;
using Random = UnityEngine.Random;

namespace Logic.Enemies
{
    /// <summary>
    /// Фабрика по генерации/уничтожении экземпляров врагов
    /// </summary>
    public class EnemyFactory
    {
        [Inject] private readonly EnemyPrefabs _enemyPrefabs;
        [Inject] private readonly EnemiesSettings _enemiesSettings;
        [Inject] private readonly EnemyPool _enemyPool;
        [Inject] private readonly AddressablesHelper _addressablesHelper;
        [Inject] private readonly CharactersDictionary _charactersDictionary;
        
        private List<EnemyType> _availableEnemies;
        private readonly Dictionary<EnemyType, DefaultObjectPool<EnemyModel>> _modelPools = new();
        private readonly Dictionary<Character, EnemyModel> _spawnedModels = new();
        private readonly HashSet<Character> _spawnedEnemies = new();
        
        public async UniTask Initialize(List<EnemyType> availableEnemies)
        {
            _availableEnemies = availableEnemies;

            foreach (var item in _availableEnemies)
            {
                if (!_modelPools.TryAdd(item, default)) continue;
                var prefab = await _addressablesHelper.GetGameObject(_enemyPrefabs.GetPrefabKey(item));
                _modelPools[item] =
                    new DefaultObjectPool<EnemyModel>(prefab.GetComponent<EnemyModel>(), startingSize: 1);
            }
        }

        public void Spawn(Vector3 position)
        {
            var enemyType = _availableEnemies[Random.Range(0, _availableEnemies.Count)];
            var enemy = _enemyPool.Spawn(_enemiesSettings.GetData(enemyType));
            enemy.transform.SetPositionAndRotation(position, Quaternion.identity);

            var model = _modelPools[enemyType].Value.Get();
            model.transform.SetParent(enemy.transform);
            model.transform.ResetLocal();
            
            enemy.Projectile.Initialize(_charactersDictionary, _enemiesSettings.GetWeaponData(enemyType));
            
            _spawnedModels.Add(enemy, model);
            _spawnedEnemies.Add(enemy);
            enemy.gameObject.SetActive(true);
        }

        public bool Despawn(Character character)
        {
            if (!_spawnedEnemies.Contains(character)) return false;
            
            _modelPools[_spawnedModels[character].Type].Value.Release(_spawnedModels[character]);
            _enemyPool.Despawn(character);
            _spawnedModels.Remove(character);
            _spawnedEnemies.Remove(character);
            return true;
        }
    }
}