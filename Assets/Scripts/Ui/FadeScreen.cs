using DG.Tweening;
using Logic.Signals;
using UnityEngine;
using Zenject;

namespace Ui
{
    /// <summary>
    /// Управление загрузочным экраном
    /// </summary>
    public class FadeScreen : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Awake()
        {
            _signalBus.Subscribe<LevelLoadedEvent>(HideFadescreen);
            _signalBus.Subscribe<LevelEndedEvent>(ShowFadescreen);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<LevelLoadedEvent>(HideFadescreen);
            _signalBus.Unsubscribe<LevelEndedEvent>(ShowFadescreen);
        }

        private void HideFadescreen()
        {
            _canvasGroup.DOFade(0f, _fadeDuration);
        }
        
        private void ShowFadescreen()
        {
            void Callback() => _signalBus.Fire<NeedReloadCoreGameplayEvent>();
            _canvasGroup.DOFade(1f, _fadeDuration).OnComplete(Callback);
        }
    }
}