using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Logic.Services.Input
{
    /// <summary>
    /// Сервисный класс, отслеживающий ввод пользователя для перемещения персонажа
    /// </summary>
    public class MovementInput : ITickable
    {
        [Inject] private readonly InputActions _inputActions;
        
        public int MovementDirection { get; private set; }
        public int RotationDirection { get; private set; }

        private InputAction _moveAction;
        
        public void Tick()
        {
            MovementDirection = Mathf.RoundToInt(_inputActions.Value.Move.ReadValue<Vector2>().y);
            RotationDirection = Mathf.RoundToInt(_inputActions.Value.Move.ReadValue<Vector2>().x);
        }
    }
}