using CustomDynamicEnums;
using Cysharp.Threading.Tasks;
using Logic.Battle.Player;
using Logic.Data.Settings;
using Logic.Levels;
using Logic.Meta;
using Logic.Services.Dictionaries;
using Logic.Signals;
using UnityEngine;
using UnityEngine.Animations;
using Utilities;
using Zenject;

namespace Logic.Characters
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly MetaToCoreBridge _metaToCoreBridge;
        [Inject] private readonly AddressablesHelper _addressablesHelper;
        [Inject] private readonly PlayerCharacterFactory _playerCharacterFactory;
        [Inject] private readonly Level _level;
        [Inject] private readonly PlayerPrefabs _playerPrefabs;
        [Inject] private readonly PlayersSettings _playersSettings;
        [Inject] private readonly CharactersDictionary _charactersDictionary;

        [SerializeField] private PositionConstraint _positionConstraint;

        public PlayerType Type => _metaToCoreBridge.PlayerType;
        public Character Character { get; private set; }
        public Transform WeaponPoint { get; private set; }

        public async UniTask Initialize()
        {
            Character = _playerCharacterFactory.Create(_playersSettings.GetPlayerData(Type));
            Character.transform.position = _level.LevelZone.PlayerSpawnPoint;
            Character.gameObject.SetActive(true);
            Character.Health.OnDied += Died;
            
            _charactersDictionary.Add(Character);
            
            _positionConstraint.AddSource(new ConstraintSource
            {
                sourceTransform = Character.transform,
                weight = 1f
            });

            var prefabKey = _playerPrefabs.GetPrefabKey(Type);
            var modelPrefab = await _addressablesHelper.GetGameObject(prefabKey);

            var model = Instantiate(modelPrefab, Character.transform);
            model.transform.ResetLocal();

            WeaponPoint = model.GetComponentInChildren<PlayerWeaponPoint>().transform;
        }

        private void Died()
        {
            Character.Health.OnDied -= Died;
            _signalBus.Fire<PlayerDiedEvent>();
        }
    }
}