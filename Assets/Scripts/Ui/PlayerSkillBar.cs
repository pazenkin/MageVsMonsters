using Cysharp.Threading.Tasks;
using DG.Tweening;
using Logic.Battle.Player;
using Logic.Core;
using Logic.Signals;
using Ui.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    /// <summary>
    /// Отображение активного оружия игрока и его таймаута
    /// </summary>
    public class PlayerSkillBar : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly PlayerSkillSprites _playerSkillSprites;
        [Inject] private readonly PlayerWeapon _playerWeapon;
        [Inject] private readonly PauseState _pauseState;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _progressBar;

        private void Awake()
        {
            _signalBus.Subscribe<NextWeaponEvent>(ChangeIcon);
            _signalBus.Subscribe<PreviousWeaponEvent>(ChangeIcon);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<NextWeaponEvent>(ChangeIcon);
            _signalBus.Unsubscribe<PreviousWeaponEvent>(ChangeIcon);

            _progressBar.DOKill();
        }

        private void Update()
        {
            if (_pauseState.Value) return;

            _progressBar.DOFillAmount(Mathf.Clamp01(_playerWeapon.Value.PercentReloadTimer), 0f);
        }

        private void ChangeIcon()
        {
            ChangeIconTask().Forget();
        }

        private async UniTask ChangeIconTask()
        {
            await UniTask.Yield();
            _icon.sprite = _playerSkillSprites.GetSprite(_playerWeapon.Value.Type);
        }
    }
}