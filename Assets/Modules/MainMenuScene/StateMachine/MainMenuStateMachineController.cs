using Photon.Pun;
using Zenject;

namespace Modules.MainMenuScene.StateMachine
{
	public class MainMenuStateMachineController : MonoBehaviourPunCallbacks
	{
		private MainMenuStateMachine _stateMachine;
		
		[Inject]
		public void Constructor(MainMenuStateMachine mainMenuStateMachine) =>
			_stateMachine = mainMenuStateMachine;

		public void Initialization()
		{
			if(!PhotonNetwork.InRoom)
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