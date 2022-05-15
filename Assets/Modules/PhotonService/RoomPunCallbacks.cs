using System;
using Photon.Pun;

namespace Modules.MainMenuScene
{
	public class RoomPunCallbacks : MonoBehaviourPunCallbacks
	{
		public event Action CreatedRoom;
		public event Action<string> CreateRoomFailed;
        
		public override void OnCreatedRoom() =>
			CreatedRoom?.Invoke();

		public override void OnCreateRoomFailed(short returnCode, string message) =>
			CreateRoomFailed?.Invoke($"{returnCode} | {message}");
	}
}