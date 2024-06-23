using System;
using System.Collections.Generic;
using Logic.Data.Settings;
using Logic.Services.Dictionaries;
using UnityEngine;
using Utilities;

namespace Logic.Battle.Projectiles
{
    /// <summary>
    /// Компонент снаряда оружия
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        
        private const float ATTACK_FREQUENCY = 1f;

        public Action OnDispose;
        public Action OnSetDamage;

        private CharactersDictionary _charactersDictionary;
        private WeaponData _weaponData;
        private bool _initialized;
        private readonly Dictionary<Collider, float> _lockedTimers = new();
        private readonly HashSet<Collider> _colliders = new();
        private readonly HashSet<Collider> _collidersForRemove = new();

        public void Initialize(CharactersDictionary charactersDictionary, WeaponData weaponData)
        {
            _charactersDictionary = charactersDictionary;
            _weaponData = weaponData;

            _initialized = true;
        }

        private void OnEnable()
        {
            _lockedTimers.Clear();
            _colliders.Clear();
            _collidersForRemove.Clear();
        }

        private void Update()
        {
            _collidersForRemove.Clear();
            foreach (var item in _colliders)
            {
                _lockedTimers[item] -= Time.deltaTime;
                if (_lockedTimers[item] <= 0f)
                {
                    _collidersForRemove.Add(item);
                }
            }
            foreach (var item in _collidersForRemove)
            {
                _lockedTimers.Remove(item);
                _colliders.Remove(item);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_initialized || _lockedTimers.ContainsKey(other) || !other.gameObject.layer.WithinMask(_layerMask)) return;

            if (_charactersDictionary.TryGetCharacter(other.gameObject, out var character))
            {
                character.DamageBuffer.SetDamage(_weaponData.Damage);
                OnSetDamage?.Invoke();
                
                _lockedTimers.Add(other, ATTACK_FREQUENCY);
                _colliders.Add(other);
            }
        }
    }
}