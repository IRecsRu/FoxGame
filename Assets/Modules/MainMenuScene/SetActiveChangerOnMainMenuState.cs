using UnityEngine;

namespace Modules.MainMenuScene
{
	public readonly struct SetActiveChangerOnMainMenuState
	{
		private readonly MainMenuState _targetState;
		private readonly GameObject _targetObject;

		public SetActiveChangerOnMainMenuState(MainMenuState state, GameObject gameObject)
		{
			_targetState = state;
			_targetObject = gameObject;
		}
        
		public void OnChangedState(MainMenuState state)
		{
			bool result = state == _targetState;
            
			if(result != _targetObject.activeSelf)
				_targetObject.SetActive(result);
		}
	}
}