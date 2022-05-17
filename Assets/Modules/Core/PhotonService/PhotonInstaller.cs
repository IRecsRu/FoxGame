using Modules.Core.Infrastructure;
using UnityEngine;
using Zenject;

namespace Modules.Core.PhotonService
{
	[RequireComponent(typeof(PhotonInitializer))]
	public class PhotonInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindPhotonSettings();
			BindPhotonInitializer();
		}
		
		private void BindPhotonSettings()
		{
			PhotonSettings settings = Resources.Load<PhotonSettings>(StaticDataPath.Photon.PhotonSettings);
			Container.Bind<PhotonSettings>().FromInstance(settings).AsSingle();
		}

		private void BindPhotonInitializer()
		{
			PhotonInitializer photonInitializer = GetComponent<PhotonInitializer>();
			Container.Bind<PhotonInitializer>().FromInstance(photonInitializer).AsSingle();
		}
	}
}