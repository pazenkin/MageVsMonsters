using System;
using UnityEngine.InputSystem;
using Zenject;

namespace Logic.Services.Input
{
    /// <summary>
    /// Сервисный класс, отслеживающий ввод пользователя для совершения атаки текущим оружием
    /// </summary>
    public class AttackInput : IInitializable, IDisposable
    {
        [Inject] private readonly InputActions _inputActions;
        
        public Action OnAttack;
        
        public void Initialize()
        {
            _inputActions.Value.Attack.performed += Attack;
        }

        public void Dispose()
        {
            _inputActions.Value.Attack.performed -= Attack;
        }

        private void Attack(InputAction.CallbackContext context)
        {
            OnAttack?.Invoke();
        }
    }
}