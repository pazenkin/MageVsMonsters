using DG.Tweening;
using Logic.Characters;
using Logic.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    /// <summary>
    /// Бар здоровья игрока
    /// </summary>
    public class PlayerHealthBar : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly Player _player;

        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _signalBus.Subscribe<LevelLoadedEvent>(Subscribe);
        }

        private void Subscribe()
        {
            _player.Character.Health.OnPercentChange += ChangeValue;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<LevelLoadedEvent>(Subscribe);
            _player.Character.Health.OnPercentChange -= ChangeValue;

            _progressBar.DOKill();
        }

        private void ChangeValue(float value)
        {
            _progressBar.DOFillAmount(Mathf.Clamp01(value), 0f);
            _text.text = $"{Mathf.RoundToInt(value * 100f)}%"; 
        }
    }
}