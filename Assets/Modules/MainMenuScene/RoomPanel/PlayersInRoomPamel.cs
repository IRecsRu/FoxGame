using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modules.Core.Infrastructure;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
#pragma warning disable CS0108, CS0114

namespace Modules.MainMenuScene.RoomPanel
{
	public class PlayersInRoomPamel : MonoBehaviourPunCallbacks
	{
		private const string PlayerPanelKey = "PlayerPanel";

		[SerializeField] private Transform _container;

		private ObjectPool<PlayerPanel> _pool;
		private readonly List<PlayerPanel> _playerPanels = new();

		private void Awake() =>
			_pool = new(PlayerPanelKey, _container);
		
		public async void UpdateInfo()
		{
			foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
				await AddPlayerInRoom(player.Value);
		}

		public override async void OnPlayerEnteredRoom(Player newPlayer) =>
			await AddPlayerInRoom(newPlayer);

		public override void OnPlayerLeftRoom(Player otherPlayer) =>
			RemovePlayerInRoom(otherPlayer);

		private async Task AddPlayerInRoom(Player player)
		{
			PlayerPanel playerPanel = _playerPanels.FirstOrDefault(p => p.ID == player.UserId);

			if(playerPanel == null)
			{
				playerPanel = await _pool.GetObject();
				_playerPanels.Add(playerPanel);
			}

			playerPanel.UpdateInfo(player);
		}

		private void RemovePlayerInRoom(Player player)
		{
			PlayerPanel playerPanel = _playerPanels.FirstOrDefault(p => p.ID == player.UserId);

			if(playerPanel != null)
			{
				playerPanel.gameObject.SetActive(false);
				_playerPanels.Remove(playerPanel);
			}
		}

		private void OnDisable()
		{
			foreach(PlayerPanel panel in _playerPanels)
				panel.gameObject.SetActive(false);

			_playerPanels.Clear();
		}

		private void OnDestroy()
		{
			_pool.Dispose();
			_playerPanels.Clear();
		}
	}
}