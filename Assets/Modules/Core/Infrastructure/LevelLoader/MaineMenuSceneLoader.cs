using System.Threading.Tasks;
using Modules.MainMenuScene.StateMachine;
using UnityEngine;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public class MaineMenuSceneLoader : MonoBehaviour, ILevelLoader
	{
		[SerializeField] private MainMenuStateMachineController _stateMachine;
		
		public async Task Initialization()
		{
			_stateMachine.Initialization();
		}
	}

}