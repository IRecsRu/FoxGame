using Photon.Pun;
using Photon.Realtime;

namespace Modules.Core.PhotonService
{
	public static class PhotonRoomCreator
	{
		public static void TryCreateRoom(string name, byte maxPlayers)
		{
			if(!Validate(name))
				return;
        
			RoomOptions roomOptions = CreateRoomOptions(maxPlayers);
			PhotonNetwork.CreateRoom(name, roomOptions, TypedLobby.Default);
		}
    
		private static RoomOptions CreateRoomOptions(byte maxPlayers)
		{
			RoomOptions roomOptions = new();
			roomOptions.PublishUserId = true;
			roomOptions.MaxPlayers =  maxPlayers;
			return roomOptions;
		}

		private static bool Validate(string name)
		{
			if(!PhotonNetwork.IsConnected)
				return false;
        
			if(string.IsNullOrEmpty(name))
				return false;
        
			return true;
		}
	}
}