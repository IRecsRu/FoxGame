using System.Threading.Tasks;
using Modules.Core.Infrastructure.LevelLoader;
using Modules.Core.PhotonService;

namespace Modules.Core.Infrastructure.States
{
	public class LoginState : IState
	{
		private readonly PhotonInitializer _photonInitializer;
		private readonly SceneLoader _sceneLoader;

		public LoginState(PhotonInitializer photonInitializer, SceneLoader sceneLoader)
		{
			_photonInitializer = photonInitializer;
			_sceneLoader = sceneLoader;
		}
    
		public async Task<bool> TryExit()
		{
			await _photonInitializer.Connect();
			return true;
		}
    
		public async Task Enter() =>
			await _sceneLoader.StartLoad(() => IJunior.TypedScenes.LoginScene.LoadAsync());
	}

}