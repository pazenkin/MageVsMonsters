using Logic.Services.Dictionaries;
using Logic.Services.Input;
using Utilities;
using Zenject;

namespace Installers
{
    /// <summary>
    /// Инсталлер Zenject для сервисных классов CoreGameplay
    /// </summary>
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputActions>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MovementInput>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<ChangeWeaponInput>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<AttackInput>().AsSingle().Lazy();

            Container.Bind<CharactersDictionary>().AsSingle().Lazy();
            Container.Bind<AddressablesHelper>().AsSingle().Lazy();
        }
    }
}