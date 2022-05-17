using System.Threading.Tasks;
using Modules.Core.Infrastructure.LevelLoader;

namespace Modules.Core.Infrastructure.States
{
	public class GameState : IState
	{
		private readonly SceneLoader _sceneLoader;

		public GameState(SceneLoader sceneLoader) =>
			_sceneLoader = sceneLoader;

		public async Task<bool> TryExit() =>
			true;

		public async Task Enter() =>
			await _sceneLoader.StartLoad(() => IJunior.TypedScenes.GameScene.LoadAsync());
	}
}