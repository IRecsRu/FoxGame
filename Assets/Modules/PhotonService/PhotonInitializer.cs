using System;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace Modules.PhotonService
{
	public class PhotonInitializer : MonoBehaviourPunCallbacks
	{
		private PhotonSettings _photonSettings;
		
		public event Action OnConnectedToMasterCompleted;
		public event Action<Exception> OnErrorConnectedToMasterCompleted;

		[Inject]
		private void Constructor(PhotonSettings photonSettings) =>
			_photonSettings = photonSettings;

		public async Task Connect()
		{
			if(PhotonNetwork.IsConnected)
				return;

			PhotonNetwork.NickName = _photonSettings.NickName;
			PhotonNetwork.GameVersion = _photonSettings.GameVersion;
			AuthenticationValues authenticationValues = new(_photonSettings.ID);
			PhotonNetwork.ConnectUsingSettings();
			
			await AwaitConnectedToMaster();
		}

		public override void OnConnectedToMaster()
		{
			if(!PhotonNetwork.InLobby)
				PhotonNetwork.JoinLobby();
			
			OnConnectedToMasterCompleted?.Invoke();
		}

		public override void OnDisconnected(DisconnectCause cause) =>
			OnErrorConnectedToMasterCompleted?.Invoke(new Exception(cause.ToString()));

		private Task AwaitConnectedToMaster()
		{
			TaskCompletionSource<string> utcs = new();
			OnConnectedToMasterCompleted += () => utcs.TrySetResult(null);
			OnErrorConnectedToMasterCompleted += exception => utcs.TrySetException(exception);
		
			if(PhotonNetwork.IsConnected)
				utcs.TrySetResult(null);
		
			return utcs.Task; 
		}
	}
}

