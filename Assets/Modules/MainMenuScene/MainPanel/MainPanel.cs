using Modules.MainMenuScene.StateMachine;
using UnityEngine;
using Zenject;

namespace Modules.MainMenuScene.MainPanel
{
	public class MainPanel : MonoBehaviour
	{
		private SetActiveChangerOnMainMenuState _activeChanger;
		
		[Inject]
		public void Constructor(MainMenuStateMachine mainMenuStateMachine) =>
			_activeChanger = new(mainMenuStateMachine,MainMenuState.Lobby, gameObject);

		private void OnDestroy() =>
			_activeChanger.Dispose();

	}
}