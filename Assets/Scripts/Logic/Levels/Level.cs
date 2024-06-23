using CustomDynamicEnums;
using Cysharp.Threading.Tasks;
using Logic.Data.Settings;
using Logic.Meta;
using UnityEngine;
using Utilities;
using Zenject;

namespace Logic.Levels
{
    /// <summary>
    /// Класс, загружающий игровой уровень
    /// </summary>
    public class Level
    {
        [Inject] private readonly MetaToCoreBridge _metaToCoreBridge;
        [Inject] private readonly AddressablesHelper _addressablesHelper;
        [Inject] private readonly LevelPrefabs _levelPrefabs;
        
        public LevelZone LevelZone { get; private set; }
        public LevelType Type => _metaToCoreBridge.LevelType;

        public async UniTask Initialize()
        {
            var prefabKey = _levelPrefabs.GetPrefabKey(Type);
            var prefab = await _addressablesHelper.GetGameObject(prefabKey);
            var levelGo = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, null);
            LevelZone = levelGo.GetComponent<LevelZone>();
        }
    }
}