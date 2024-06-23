using System;
using Logic.Core;
using Logic.Services.Input;
using Zenject;

namespace Logic.Battle.Player
{
    /// <summary>
    /// Система совершения игроком выстрелов из текущего оружия
    /// </summary>
    public class PlayerAttack : IInitializable, IDisposable
    {
        [Inject] private readonly AttackInput _attackInput;
        [Inject] private readonly PlayerWeapon _playerWeapon;
        [Inject] private readonly PauseState _pauseState;

        public void Initialize()
        {
            _attackInput.OnAttack += Fire;
        }

        public void Dispose()
        {
            _attackInput.OnAttack -= Fire;
        }

        private void Fire()
        {
            if (_pauseState.Value) return;
            _playerWeapon.Value?.Fire();
        }
    }
}