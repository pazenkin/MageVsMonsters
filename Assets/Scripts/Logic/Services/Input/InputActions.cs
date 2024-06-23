using System;
using Zenject;

namespace Logic.Services.Input
{
    /// <summary>
    /// Менеджер ввода данных
    /// </summary>
    public class InputActions : IInitializable, IDisposable
    {
        public InputActions()
        {
            _controls = new GameControls();
            Value = _controls.Prototype;
        }
        
        public GameControls.PrototypeActions Value { get; private set; }
        
        private readonly GameControls _controls;

        public void Initialize()
        {
            _controls.Enable();
        }

        public void Dispose()
        {
            _controls.Disable();
        }
    }
}