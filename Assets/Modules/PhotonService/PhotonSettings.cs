using UnityEngine;

namespace Modules.PhotonService
{
	[CreateAssetMenu(fileName = "PhotonSettings", menuName = "Photon/Settings")]
	public class PhotonSettings : ScriptableObject
	{
		[SerializeField] private string _gameVersion = "0.0.1";
		[SerializeField] private string _nickName = "TestPlayer";

		public string GameVersion => _gameVersion;
		public string NickName => _nickName;
	}

}