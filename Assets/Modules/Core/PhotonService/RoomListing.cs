using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Core.PhotonService
{
	[RequireComponent(typeof(Button))]
	public class RoomListing : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;
		private Button _button;
		
		private RoomInfo _roomInfo;
		private void Awake()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnButtonClick);
		}
		
		private void OnButtonClick() =>
			PhotonNetwork.JoinRoom(_roomInfo.Name);

		public void SetRoomInfo(RoomInfo roomInfo)
		{
			_roomInfo = roomInfo;
			_text.text = roomInfo.Name;
		}

		public void UpdateRoomInfo(RoomInfo roomInfo)
		{
		}
	}
}