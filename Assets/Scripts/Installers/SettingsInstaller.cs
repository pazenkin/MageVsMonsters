using Logic.Data.Settings;
using UnityEngine;
using Zenject;

namespace Installers
{
    /// <summary>
    /// Инсталлер Zenject для игровых настроек и конфигов
    /// </summary>
    // [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerWeaponSettings _playerWeaponSettings;
        [SerializeField] private LevelPrefabs _levelPrefabs;
        [SerializeField] private PoolPrefabs _poolPrefabs;
        [SerializeField] private PlayerPrefabs _playerPrefabs;
        [SerializeField] private PlayersSettings _playersSettings;
        [SerializeField] private EnemySpawningSettings _enemySpawningSettings;
        [SerializeField] private EnemyPrefabs _enemyPrefabs;
        [SerializeField] private EnemiesSettings _enemiesSettings;
        [SerializeField] private ProjectilePrefabs _projectilePrefabs;
        
        public override void InstallBindings()
        {
            Container.BindInstances(
                _playerWeaponSettings,
                _levelPrefabs,
                _poolPrefabs,
                _playerPrefabs,
                _playersSettings,
                _enemySpawningSettings,
                _enemyPrefabs,
                _enemiesSettings,
                _projectilePrefabs);
        }
    }
}