using Logic.Signals;
using UnityEngine;
using Zenject;

namespace Ui
{
    /// <summary>
    /// Окно окончания игры
    /// </summary>
    public class GameOverWindow : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private GameObject _windowContent;

        private void Awake()
        {
            _signalBus.Subscribe<PlayerDiedEvent>(ShowGameOverWindow);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PlayerDiedEvent>(ShowGameOverWindow);
        }

        private void ShowGameOverWindow()
        {
            _windowContent.SetActive(true);
        }
        
        public void Restart()
        {
            _signalBus.Fire<LevelEndedEvent>();
        }
    }
}