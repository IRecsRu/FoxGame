using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Core.Infrastructure;
using Modules.Core.PhotonService;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Modules.MainMenuScene.MainPanel
{
	public class RoomListingsPanel : MonoBehaviourPunCallbacks
	{
		private const string RoomListingKey = "RoomListing";

		[SerializeField] private Transform _container;

		private ObjectPool<RoomListing> _pool;
		private readonly Dictionary<string, RoomListing> _listings = new();

		private void Awake() =>
			_pool = new(RoomListingKey, _container);

		public override async void OnRoomListUpdate(List<RoomInfo> roomList)
		{
			foreach(RoomInfo info in roomList)
			{
				if(_listings.ContainsKey(info.Name))
					ChangeRoomState(info);
				else
					await CreateRoom(info);
			}
		}

		private void ChangeRoomState(RoomInfo info)
		{
			if(info.RemovedFromList)
				TryDestroyRoom(info);
			else
				UpdateRoom(info);
		}

		private void UpdateRoom(RoomInfo info) =>
			_listings[info.Name].UpdateRoomInfo(info);

		private void TryDestroyRoom(RoomInfo info)
		{
			_listings[info.Name].gameObject.SetActive(false);
			_listings.Remove(info.Name);
		}

		private async Task CreateRoom(RoomInfo info)
		{
			if(info.RemovedFromList)
				return;

			var listing = await _pool.GetObject();
			_listings.Add(info.Name, listing);
			listing.SetRoomInfo(info);
		}

		public override void OnDisable()
		{
			foreach(KeyValuePair<string, RoomListing> panel in _listings)
				panel.Value.gameObject.SetActive(false);
			
			_listings.Clear();
		}

		private void OnDestroy()
		{
			_pool.Dispose();
			_listings.Clear();
		}
	}
}