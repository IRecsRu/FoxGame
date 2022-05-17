using Modules.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Modules.MainMenuScene
{
	public class GameSceneInstallers : MonoInstaller
	{
		[SerializeField] private PlayerCreator _playerCreator;
		
		public override void InstallBindings()
		{
			Container.Bind<PlayerCreator>().FromInstance(_playerCreator).AsSingle();
			BindInputService();
		}
        
		private void BindInputService()
		{
			if (Application.isMobilePlatform)
				Container.Bind<IInputService>().To<MobileInputService>().FromNew().AsSingle();
			else
				Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle();
		}
	}
}