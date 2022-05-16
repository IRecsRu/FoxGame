using System;

namespace Modules.MainMenuScene.StateMachine
{
	public class MainMenuStateMachine
	{
		public event Action<MainMenuState> ChangedState;
		
		public MainMenuState State { get; private set; }

		public void ChangeState(MainMenuState state)
		{
			if(state == State)
				return;

			State = state;
			ChangedState?.Invoke(State);
		}
	}

	public enum MainMenuState
	{
		Idle,
		Lobby,
		Room,
	}
}