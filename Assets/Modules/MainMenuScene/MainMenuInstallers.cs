using Zenject;

namespace Modules.MainMenuScene
{
    public class MainMenuInstallers : MonoInstaller
    {
        public override void InstallBindings() =>
            Container.Bind<MainMenuStateMachine>().FromNew().AsSingle();

    }
}
