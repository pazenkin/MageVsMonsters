using Logic.Battle.Projectiles;
using Logic.Data.Settings;
using Logic.Movement;
using Logic.Signals;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    /// <summary>
    /// Основная информация о персонаже
    /// </summary>
    public class Character : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private bool _hasMeleeAttack;
        [SerializeField, ShowIf("_hasMeleeAttack")] private Projectile _projectile;
        
        public Armor Armor { get; private set; }
        public Health Health { get; private set; }
        public DamageBuffer DamageBuffer { get; private set; }

        public Projectile Projectile => _projectile;

        private bool _hasMovementComponent;
        private ISpeedable _movementComponent;
        
        public void Initialize(MainCharacterData mainCharacterData)
        {
            Health = new Health(mainCharacterData.Health);
            Health.OnDied += Died;
            Armor = new Armor();
            DamageBuffer = new DamageBuffer(Health, Armor);

            if (!_hasMovementComponent)
            {
                _movementComponent = GetComponent<ISpeedable>();
                _hasMovementComponent = _movementComponent != null;
            }
            if (_hasMovementComponent)
            {
                _movementComponent!.Speed = mainCharacterData.Speed;
            }
        }

        private void Died()
        {
            Health.OnDied -= Died;
            _signalBus.Fire(new CharacterDiedEvent { Character = this });
        }
    }
}