using System;
using Cysharp.Threading.Tasks;
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
		private void Constructor(PhotonSettings photonSettings)
		{
			_photonSettings = photonSettings;
			Connect();
		}


		public async UniTask Connect()
		{
			if(PhotonNetwork.IsConnected)
				return;

			PhotonNetwork.NickName = _photonSettings.NickName;
			PhotonNetwork.GameVersion = _photonSettings.GameVersion;
			PhotonNetwork.ConnectUsingSettings();
		
			await AwaitConnectedToMaster();
		}

		public override void OnConnectedToMaster()
		{
			OnConnectedToMasterCompleted?.Invoke();
			
			if(!PhotonNetwork.InLobby)
				PhotonNetwork.JoinLobby();
		}

		public override void OnDisconnected(DisconnectCause cause) =>
			OnErrorConnectedToMasterCompleted?.Invoke(new Exception(cause.ToString()));

		private UniTask AwaitConnectedToMaster()
		{
			UniTaskCompletionSource utcs = new();
			OnConnectedToMasterCompleted += () => utcs.TrySetResult();
			OnErrorConnectedToMasterCompleted += exception => utcs.TrySetException(exception);
		
			if(PhotonNetwork.IsConnected)
				utcs.TrySetResult();
		
			return utcs.Task; 
		}
	}
}

