using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace IJunior.TypedScenes
{
    public abstract class TypedScene
    {
        protected static async UniTask<SceneInstance> LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            await Addressables.DownloadDependenciesAsync(sceneName);
            SceneInstance sceneInstance = await Addressables.LoadSceneAsync(sceneName, loadSceneMode).ToUniTask();
            return sceneInstance;
        }
    }
}
