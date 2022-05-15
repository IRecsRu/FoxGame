using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Infrastructure.AddressablesServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Modules.MainMenuScene
{
	public class PlayersInRoomPamel : MonoBehaviourPunCallbacks
	{
		private const string PlayerPanelKey = "PlayerPanel";

		[SerializeField] private Transform _container;

		private readonly AddressablesGameObjectLoader _gameObjectLoader = new();
		private readonly List<PlayerPanel> _playerPanels = new();

		public async void UpdateInfo()
		{
			foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
				await AddPlayerInRoom(player.Value);
		}

		public override async void OnPlayerEnteredRoom(Player newPlayer) =>
			await AddPlayerInRoom(newPlayer);

		public override void OnPlayerLeftRoom(Player otherPlayer) =>
			RemovePlayerInRoom(otherPlayer);

		private async UniTask AddPlayerInRoom(Player player)
		{
			PlayerPanel playerPanel = _playerPanels.FirstOrDefault(p => p.Name == player.NickName);

			if(playerPanel == null)
			{
				playerPanel = await _gameObjectLoader.Instantiate<PlayerPanel>(_container, PlayerPanelKey);
				_playerPanels.Add(playerPanel);
			}

			playerPanel.UpdateInfo(player);
		}

		private void RemovePlayerInRoom(Player player)
		{
			PlayerPanel playerPanel = _playerPanels.FirstOrDefault(p => p.Name == player.NickName);

			if(playerPanel != null)
			{
				_playerPanels.Remove(playerPanel);
				Destroy(playerPanel);
			}
		}

		private void OnDisable()
		{
			for( int i = 0; i <_playerPanels.Count; i++ )
			{
				GameObject temp = _playerPanels[i].gameObject;
				_playerPanels.RemoveAt(i);
				Destroy(temp);
				i -= 1;
			}
		}
	}
}