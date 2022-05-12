using System;
using Cysharp.Threading.Tasks;
using Modules.Infrastructure.AddressablesServices;
using Modules.Infrastructure.LevelLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Modules.Infrastructure
{
  public class SceneLoader
  {
    private const string LoadingCurtainKey = "LoadingCurtain";
    
    private  LoadingCurtain _loadingCurtain;

    public SceneLoader() =>
      BindLoadingCurtain();

    private async UniTaskVoid BindLoadingCurtain()
    {
      AddressablesGameObjectLoader loader = new AddressablesGameObjectLoader();
      GameObject loadingCurtainObject = await loader.InstantiateGameObject(null, LoadingCurtainKey);
      _loadingCurtain = loadingCurtainObject.GetComponent<LoadingCurtain>();
      EndLoad();
    }
    
    public async UniTask StartLoad(Func<UniTask<SceneInstance>> task)
    {
      await CheckLoadingCurtain();
      _loadingCurtain.Show();
      
      SceneInstance sceneInstance = await task.Invoke();
      
      await Initialization(sceneInstance);
    }

    private async UniTask CheckLoadingCurtain()
    {
      while(_loadingCurtain == null)
        await UniTask.Delay(100);
    }
    
    private async UniTask Initialization(SceneInstance sceneInstance)
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