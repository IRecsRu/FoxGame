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
        
        private SetActiveChangerOnMainMenuState _activeChanger;
        
        [Inject]
        public void Constructor(MainMenuStateMachine mainMenuStateMachine) =>
            _activeChanger = new(mainMenuStateMachine,MainMenuState.Room, gameObject);

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
            PhotonNetwork.LeaveRoom(true);

        private void OnDestroy()
        {
            _leaveButton.onClick.RemoveListener(OnLeaveRoom);
            _activeChanger.Dispose();
        }
    }

}
