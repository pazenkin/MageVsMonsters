using System;
using Cysharp.Threading.Tasks;
using Logic.Core;
using Logic.Data.Settings;
using Logic.Services.Dictionaries;
using Logic.Signals;
using Utilities;
using Zenject;

namespace Logic.Battle.Player
{
    /// <summary>
    /// Переключение текущего оружия игрока и ссылка на него
    /// </summary>
    public class PlayerWeapon : IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly Characters.Player _player;
        [Inject] private readonly PlayerWeaponSettings _playerWeaponSettings;
        [Inject] private readonly PauseState _pauseState;
        [Inject] private readonly AddressablesHelper _addressablesHelper;
        [Inject] private readonly ProjectilePrefabs _projectilePrefabs;
        [Inject] private readonly CharactersDictionary _charactersDictionary;

        public BasePlayerWeapon Value => _currentWeapon?.Data;
        
        private readonly CircularDoublyLinkedList<BasePlayerWeapon> _weapons = new();
        private DoublyNode<BasePlayerWeapon> _currentWeapon;
        
        public async UniTask Initialize()
        {
            _signalBus.Subscribe<NextWeaponEvent>(NextWeapon);
            _signalBus.Subscribe<PreviousWeaponEvent>(PreviousWeapon);
            
            var allWeapons = new AllWeapons();
            foreach (var item in allWeapons.Values)
            {
                if (!_playerWeaponSettings.WeaponIsAvailable(_player.Type, item.Type)) continue;
                _weapons.Add(item);
            }
            _currentWeapon = _weapons.Head;
            
            foreach (var item in _weapons)
            {
                var config = _playerWeaponSettings.GetData(item.Type);
                await item.Initialize(new BasePlayerWeapon.PreparingData
                {
                    Player = _player,
                    WeaponData = config.WeaponData,
                    ProjectilePrefabs = _projectilePrefabs,
                    AddressablesHelper = _addressablesHelper,
                    CharactersDictionary = _charactersDictionary
                });
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<NextWeaponEvent>(NextWeapon);
            _signalBus.Unsubscribe<PreviousWeaponEvent>(PreviousWeapon);
        }

        private void NextWeapon()
        {
            if (_pauseState.Value) return;
            _currentWeapon = _currentWeapon?.Next;
        }

        private void PreviousWeapon()
        {
            if (_pauseState.Value) return;
            _currentWeapon = _currentWeapon?.Previous;
        }
    }
}