using System.Threading.Tasks;
using Modules.Infrastructure.LevelLoader;
using Modules.PhotonService;

namespace Modules.Infrastructure.States
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