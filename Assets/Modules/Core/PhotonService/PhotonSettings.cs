using System;
using UnityEngine;

namespace Modules.Core.PhotonService
{
	[CreateAssetMenu(fileName = "PhotonSettings", menuName = "Photon/Settings")]
	public class PhotonSettings : ScriptableObject
	{
		private const string PlayerID = "PlayerID";
		
		[SerializeField] private string _gameVersion = "0.0.1";
		[SerializeField] private string _nickName = "TestPlayer";

		public string GameVersion => _gameVersion;
		public string NickName => _nickName;
		public string ID => GetID();

		public void SetNickName(string nickName) =>
			_nickName = nickName;

		private string GetID()
		{
			if(!PlayerPrefs.HasKey(PlayerID))
				PlayerPrefs.SetString(PlayerID, Guid.NewGuid().ToString());

			return PlayerPrefs.GetString(PlayerID);
		}
	}

}