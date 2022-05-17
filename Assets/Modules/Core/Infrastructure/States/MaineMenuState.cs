using System.Threading.Tasks;
using Modules.Core.Infrastructure.LevelLoader;

namespace Modules.Core.Infrastructure.States
{
	public class MaineMenuState : IState
	{
		private readonly SceneLoader _sceneLoader;

		public MaineMenuState(SceneLoader sceneLoader) =>
			_sceneLoader = sceneLoader;

		public async Task<bool> TryExit() =>
			true;

		public async Task Enter() =>
			await _sceneLoader.StartLoad(() => IJunior.TypedScenes.MainMenuScene.LoadAsync());
	}

}