using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace IJunior.TypedScenes
{
    public abstract class TypedScene
    {
        protected static async Task<SceneInstance> LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            await Addressables.DownloadDependenciesAsync(sceneName).Task;
            SceneInstance sceneInstance = await Addressables.LoadSceneAsync(sceneName, loadSceneMode).Task;
            return sceneInstance;
        }
    }
}
