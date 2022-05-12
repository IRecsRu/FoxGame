using Modules.Infrastructure.States;
using Zenject;

namespace Modules.Infrastructure
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<SceneLoader>().FromNew().AsSingle();
			Container.Bind<GameStateMachine>().FromNew().AsSingle();
			GameStatesRegistration();
		}

		private void GameStatesRegistration()
		{
			GameStateMachine gameStateMachine = Container.Resolve<GameStateMachine>();
			SceneLoader sceneLoader = Container.Resolve<SceneLoader>();
			
			gameStateMachine.TryAddState(typeof(BootstrapState), new BootstrapState(gameStateMachine, sceneLoader));
		}
	}
}