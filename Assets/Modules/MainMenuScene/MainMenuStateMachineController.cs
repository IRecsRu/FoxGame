using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Modules.MainMenuScene
{
	public class MainMenuStateMachineController : MonoBehaviourPunCallbacks
	{
		private MainMenuStateMachine _stateMachine;
		
		[Inject]
		public void Constructor(MainMenuStateMachine mainMenuStateMachine) =>
			_stateMachine = mainMenuStateMachine;

		private void Start()
		{
			_stateMachine.ChangeState(MainMenuState.Lobby);
			return;
			if(PhotonNetwork.InLobby)
				_stateMachine.ChangeState(MainMenuState.Lobby);
			else
				_stateMachine.ChangeState(MainMenuState.Room);
		}

		public override void OnCreatedRoom() =>
			_stateMachine.ChangeState(MainMenuState.Room);

		public override void OnJoinedRoom() =>
			_stateMachine.ChangeState(MainMenuState.Room);

		public override void OnLeftRoom() =>
			_stateMachine.ChangeState(MainMenuState.Lobby);
	}
}