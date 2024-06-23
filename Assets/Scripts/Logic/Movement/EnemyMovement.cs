using Logic.Characters;
using Logic.Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Logic.Movement
{
    /// <summary>
    /// Система перемещения врагов
    /// </summary>
    public class EnemyMovement : MonoBehaviour, ISpeedable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly Player _player;
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                _navMeshAgent.speed = value;
            }
        }

        private float _speed;

        private void OnEnable()
        {
            SetDestination(_player.transform.position);
            _signalBus.Subscribe<ChangePlayerPositionEvent>(SetDestination);
            _signalBus.Subscribe<PlayerDiedEvent>(StopMovement);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ChangePlayerPositionEvent>(SetDestination);
            _signalBus.Unsubscribe<PlayerDiedEvent>(StopMovement);
        }

        private void SetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }

        private void SetDestination(ChangePlayerPositionEvent args)
        {
            SetDestination(args.NewPosition);
        }

        private void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}