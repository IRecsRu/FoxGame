using UnityEngine;
using Zenject;

namespace Modules.MainMenuScene
{
	public class MainPanel : MonoBehaviour
	{
		private MainMenuStateMachine _stateMachine;
		private SetActiveChangerOnMainMenuState _activeChanger;
		
		[Inject]
		public void Constructor(MainMenuStateMachine mainMenuStateMachine)
		{
			_stateMachine = mainMenuStateMachine;
			_activeChanger = new(MainMenuState.Lobby, gameObject);
			_stateMachine.ChangedState += _activeChanger.OnChangedState;
		}

		private void OnDestroy()
		{
			_stateMachine.ChangedState -= _activeChanger.OnChangedState;
		}
	}
}