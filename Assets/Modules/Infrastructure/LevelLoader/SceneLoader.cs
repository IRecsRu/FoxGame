using System;
using System.Threading.Tasks;
using Modules.Infrastructure.AddressablesServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Modules.Infrastructure.LevelLoader
{
	public class SceneLoader
	{
		private const string LoadingCurtainKey = "LoadingCurtain";
		private LoadingCurtain _loadingCurtain;

		public SceneLoader() =>
			BindLoadingCurtain();

		private async void BindLoadingCurtain()
		{
			AddressablesGameObjectLoader loader = new();
			GameObject loadingCurtainObject = await loader.InstantiateGameObject(null, LoadingCurtainKey);
			_loadingCurtain = loadingCurtainObject.GetComponent<LoadingCurtain>();
			EndLoad();
		}

		public async Task StartLoad(Func<Task<SceneInstance>> task)
		{
			await CheckLoadingCurtain();
			_loadingCurtain.Show();

			SceneInstance sceneInstance = await task.Invoke();

			await Initialization(sceneInstance);
		}

		private async Task CheckLoadingCurtain()
		{
			while(_loadingCurtain == null)
				await Task.Delay(100);
		}

		private async Task Initialization(SceneInstance sceneInstance)
		{
			Scene scene = sceneInstance.Scene;

			if(!TypeFinderOnScene.TryGetType(out ILevelLoader maineMenuLoader, scene))
				Debug.LogError($"There is no {nameof(ILevelLoader)} on the Scene {scene.name}");
			
			await maineMenuLoader.Initialization();
			
			Addressables.Release(sceneInstance);
			EndLoad();
		}

		private void EndLoad()
		{
			_loadingCurtain.Hide();
		}
	}

}