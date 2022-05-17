using System.Threading.Tasks;
using Modules.CameraLogic;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.PlayerLogic;
using Modules.Core.Services.Input;
using Modules.Core.UI;
using UnityEngine;
using Zenject;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public class GameSceneLoader : MonoBehaviour, ILevelLoader
	{
		private const string HudKey = "Hud";

		private IInputService _inputService;
		private PlayerCreator _playerCreator;
		private readonly AddressablesGameObjectLoader _loader = new();
		
		[Inject]
		private void Constructor(IInputService inputService, PlayerCreator playerCreator)
		{
			_inputService = inputService;
			_playerCreator = playerCreator;
		}

		public async Task Initialization()
		{
			Debug.Log(_inputService);
			
			GameObject hud = await _loader.InstantiateGameObject(null, HudKey);
			
			GameObject player = await _playerCreator.Create();
			
			player.AddComponent<PlayerMove>().Constructor(_inputService);
			player.GetComponent<PlayerAttack>().Constructor(_inputService);
			
			PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
			hud.GetComponent<ActorUI>().Construct(playerHealth);
			
			Camera.main.GetComponent<CameraFollow>().Follow(player);
		}
	}
}