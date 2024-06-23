using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Core;
using Logic.Services.Input;
using Logic.Signals;
using UnityEngine;
using Zenject;

namespace Logic.Movement
{
    /// <summary>
    /// Система перемещения игрока
    /// </summary>
    public class PlayerMovement : MonoBehaviour, ISpeedable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly MovementInput _movementInput;
        [Inject] private readonly PauseState _pauseState;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _rotationSpeedMultiplier;

        public float Speed { get; set; }
        
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _signalBus.Subscribe<LevelLoadedEvent>(StartMovement);
            _signalBus.Subscribe<PlayerDiedEvent>(StopMovement);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<LevelLoadedEvent>(StartMovement);
            _signalBus.Unsubscribe<PlayerDiedEvent>(StopMovement);

            StopMovement();
        }

        private void StartMovement()
        {
            Movement().Forget();
        }

        private void StopMovement()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private async UniTask Movement()
        {
            _cts = new CancellationTokenSource();

            while (true)
            {
                var hasMove = false;
                if (!_pauseState.Value)
                {
                    if (_movementInput.MovementDirection != 0)
                    {
                        _characterController.Move(transform.forward * _movementInput.MovementDirection * Speed *
                                                  Time.deltaTime);
                        hasMove = true;
                    }

                    if (_movementInput.RotationDirection != 0)
                    {
                        transform.Rotate(Vector3.up,
                            _movementInput.RotationDirection * Speed * _rotationSpeedMultiplier * Time.deltaTime);
                    }
                }

                await UniTask.Yield(cancellationToken: _cts.Token);
                if (hasMove)
                {
                    _signalBus.Fire(new ChangePlayerPositionEvent
                    {
                        NewPosition = transform.position
                    });
                }
            }
        }
    }
}