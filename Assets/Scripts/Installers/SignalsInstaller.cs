using Logic.Signals;
using Zenject;

namespace Installers
{
    /// <summary>
    /// Инсталлер Zenject для глобальных событий
    /// </summary>
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<PlayerDiedEvent>();
            Container.DeclareSignal<CharacterDiedEvent>();
            Container.DeclareSignal<NextWeaponEvent>();
            Container.DeclareSignal<PreviousWeaponEvent>();
            Container.DeclareSignal<LevelLoadedEvent>();
            Container.DeclareSignal<LevelEndedEvent>();
            Container.DeclareSignal<ChangePlayerPositionEvent>();
        }
    }
}