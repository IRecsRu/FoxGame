using Modules.Core.Infrastructure.LevelLoader;
using Modules.Core.PhotonService;
using Zenject;

namespace Modules.Core.Infrastructure.States
{
	public class GameStateMachineInstaller : MonoInstaller
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
			PhotonInitializer photonInitializer = Container.Resolve<PhotonInitializer>();
			
			gameStateMachine.TryAddState(typeof(BootstrapState), new BootstrapState(gameStateMachine, sceneLoader));
			gameStateMachine.TryAddState(typeof(LoginState), new LoginState(photonInitializer, sceneLoader));
			gameStateMachine.TryAddState(typeof(MaineMenuState), new MaineMenuState(sceneLoader));
			gameStateMachine.TryAddState(typeof(GameState), new GameState(sceneLoader));
		}
	}

}