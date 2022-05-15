using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.MainMenuScene
{
    public class RoomPanel : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _roomName;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _leaveButton;
        [SerializeField] private PlayersInRoomPamel _playersInRoomPamel;
        
        private MainMenuStateMachine _stateMachine;
        private SetActiveChangerOnMainMenuState _activeChanger;
        
        [Inject]
        public void Constructor(MainMenuStateMachine mainMenuStateMachine)
        {
            _stateMachine = mainMenuStateMachine;
            _activeChanger = new(MainMenuState.Room, gameObject);
            _stateMachine.ChangedState += _activeChanger.OnChangedState;
        }

        private void Awake()
        {
            _leaveButton.onClick.AddListener(OnLeaveRoom);
        }

        public override void OnEnable()
        {
            Room room = PhotonNetwork.CurrentRoom;

            _roomName.text = room.Name;
            _playersInRoomPamel.UpdateInfo();
        }
        
        private void OnLeaveRoom() =>
            PhotonNetwork.LeaveRoom();

        private void OnDestroy()
        {
            _leaveButton.onClick.RemoveListener(OnLeaveRoom);
            _stateMachine.ChangedState -= _activeChanger.OnChangedState;
        }
    }

}
