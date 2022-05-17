using Modules.Core.Infrastructure.States;
using Modules.MainMenuScene.StateMachine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.MainMenuScene.RoomPanel
{
    public class RoomPanel : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _roomName;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _leaveButton;
        [SerializeField] private PlayersInRoomPamel _playersInRoomPamel;

        private GameStateMachine _gameStateMachine;
        private SetActiveChangerOnMainMenuState _activeChanger;
        
        [Inject]
        public void Constructor(MainMenuStateMachine mainMenuStateMachine, GameStateMachine gameStateMachine)
        {
            _activeChanger = new(mainMenuStateMachine, MainMenuState.Room, gameObject);
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _connectButton.onClick.AddListener(OnJoin);
            _leaveButton.onClick.AddListener(OnLeaveRoom);
        }
        private async void OnJoin()
        {
            _connectButton.onClick.RemoveListener(OnJoin);
            _leaveButton.onClick.RemoveListener(OnLeaveRoom);
            await _gameStateMachine.Enter<GameState>();
        }

        public override void OnEnable()
        {
            Room room = PhotonNetwork.CurrentRoom;

            _roomName.text = room.Name;
            _playersInRoomPamel.UpdateInfo();
        }
        
        private void OnLeaveRoom() =>
            PhotonNetwork.LeaveRoom(true);

        private void OnDestroy()
        {
            _leaveButton.onClick.RemoveListener(OnLeaveRoom);
            _activeChanger.Dispose();
        }
    }

}
