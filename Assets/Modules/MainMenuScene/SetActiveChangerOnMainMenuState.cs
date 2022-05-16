using System;
using Modules.MainMenuScene.StateMachine;
using UnityEngine;

namespace Modules.MainMenuScene
{
	public readonly struct SetActiveChangerOnMainMenuState : IDisposable
	{
		private readonly MainMenuStateMachine _stateMachine;
		private readonly GameObject _targetObject;
		private readonly MainMenuState _targetState;

		public SetActiveChangerOnMainMenuState(MainMenuStateMachine stateMachine, MainMenuState state, GameObject gameObject)
		{
			_stateMachine = stateMachine;
			_targetState = state;
			_targetObject = gameObject;
			_stateMachine.ChangedState += OnChangedState;
		}

		private void OnChangedState(MainMenuState state)
		{
			bool result = state == _targetState;
            
			if(result != _targetObject.activeSelf)
				_targetObject.SetActive(result);
		}
		
		public void Dispose() =>
			_stateMachine.ChangedState -= OnChangedState;
	}
}