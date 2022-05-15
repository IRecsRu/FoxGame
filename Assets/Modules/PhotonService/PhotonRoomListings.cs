using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Infrastructure.AddressablesServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Modules.PhotonService
{
	public class PhotonRoomListings : MonoBehaviourPunCallbacks
	{
		private const string RoomListingKey = "RoomListing";

		[SerializeField] private Transform _container;

		private readonly AddressablesGameObjectLoader _loader = new();
		private readonly Dictionary<string, RoomListing> _listings = new();

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
			RoomListing listing = _listings[info.Name];
			_listings.Remove(info.Name);
			Destroy(listing);
		}

		private async UniTask CreateRoom(RoomInfo info)
		{
			if(info.RemovedFromList)
				return;
			
			RoomListing listing = await _loader.Instantiate<RoomListing>(_container, RoomListingKey);
			_listings.Add(info.Name, listing);
			listing.SetRoomInfo(info);
		}
	}
}