using System;
using Modules.Core.PhotonService;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MainMenuScene.MainPanel
{
    public class CreateRoomPanel : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField _roomName;
        [SerializeField] private TMP_InputField _maxPlayers;
        [SerializeField] private Button _createRoomButton;

        private void Awake() =>
            _createRoomButton.onClick.AddListener(TryCreateRoom);

        private void TryCreateRoom()
        {
            if(!Validate())
                return;

            PhotonRoomCreator.TryCreateRoom(_roomName.text, GetMaxPlayers());
        }
    
        private byte GetMaxPlayers()
        {
            if(string.IsNullOrEmpty(_maxPlayers.text))
                return 0;

            return Convert.ToByte(_maxPlayers.text);
        }

        private bool Validate()
        {
            if(string.IsNullOrEmpty(_roomName.text))
                return false;
        
            return true;
        }

        
        
        public override void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom " + PhotonNetwork.CurrentRoom.Name);
            gameObject.SetActive(false);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom " + PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnCreateRoomFailed(short returnCode, string message) =>
            Debug.Log(returnCode +" || " + message);
    }


}
