using Logic.Meta;
using Zenject;

namespace Installers
{
    /// <summary>
    /// Инсталлер Zenject для псевдо-меты
    /// </summary>
    public class MetaInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MetaToCoreBridge>().AsSingle().Lazy();
        }
    }
}