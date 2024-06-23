using CustomDynamicEnums;
using Cysharp.Threading.Tasks;
using Logic.Battle.Projectiles;
using Logic.Data.Settings;
using Logic.Services.Dictionaries;
using UnityEngine;
using Utilities;

namespace Logic.Battle.Player
{
    public abstract class BasePlayerWeapon
    {
        public abstract PlayerWeaponType Type { get; }
        public float PercentReloadTimer => _reloadTimer / _weaponData.ReloadTime;
        
        protected DefaultObjectPool<Projectile> _pool;
        protected WeaponData _weaponData;
        protected Characters.Player _player;
        protected CharactersDictionary _charactersDictionary;
        
        public class PreparingData
        {
            public AddressablesHelper AddressablesHelper;
            public ProjectilePrefabs ProjectilePrefabs;
            public CharactersDictionary CharactersDictionary;
            public WeaponData WeaponData;
            public Characters.Player Player;
        }

        private float _reloadTimer;

        public async UniTask Initialize(PreparingData preparingData)
        {
            var prefabKey = preparingData.ProjectilePrefabs.GetPrefabKey(Type);
            var prefab = await preparingData.AddressablesHelper.GetGameObject(prefabKey);
            _pool = new DefaultObjectPool<Projectile>(prefab.GetComponent<Projectile>());
            _weaponData = preparingData.WeaponData;
            _player = preparingData.Player;
            _charactersDictionary = preparingData.CharactersDictionary;
        }

        public void Fire()
        {
            if (_reloadTimer > 0f) return;
            ReloadTimerTask().Forget();
            CreateProjectile();
        }

        protected virtual void CreateProjectile()
        {
            var projectile = _pool.Value.Get();
            projectile.transform.position = GetProjectilePosition();
            projectile.transform.rotation = GetProjectileRotation();
            projectile.OnDispose = () => ReturnToPool(projectile);
            projectile.Initialize(_charactersDictionary, _weaponData);
            projectile.gameObject.SetActive(true);
        }
        
        protected abstract Vector3 GetProjectilePosition();
        protected virtual Quaternion GetProjectileRotation() => Quaternion.identity;
        
        protected void ReturnToPool(Projectile projectile)
        {
            projectile.OnDispose = null;
            _pool.Value.Release(projectile);
        }

        private async UniTask ReloadTimerTask()
        {
            _reloadTimer = _weaponData.ReloadTime;

            do
            {
                _reloadTimer -= Time.deltaTime;
                await UniTask.Yield();
            } while (_reloadTimer > 0f);

            _reloadTimer = 0f;
        }
    }
}