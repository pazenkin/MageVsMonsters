using System;
using Logic.Signals;
using UnityEngine.InputSystem;
using Zenject;

namespace Logic.Services.Input
{
    /// <summary>
    /// Сервисный класс, отслеживающий ввод пользователя для смены текущего оружия
    /// </summary>
    public class ChangeWeaponInput : IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly InputActions _inputActions;
        
        public void Initialize()
        {
            _inputActions.Value.ChangeSkill.performed += OnChangeWeapon;
        }

        public void Dispose()
        {
            _inputActions.Value.ChangeSkill.performed -= OnChangeWeapon;
        }

        private void OnChangeWeapon(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() > 0f)
            {
                _signalBus.Fire<NextWeaponEvent>();
            }
            else
            {
                _signalBus.Fire<PreviousWeaponEvent>();
            }
        }
    }
}