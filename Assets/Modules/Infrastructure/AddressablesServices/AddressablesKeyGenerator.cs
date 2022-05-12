using Cysharp.Threading.Tasks;

namespace Modules.Infrastructure.AddressablesServices
{
    public static class AddressablesKeyGenerator
    {
        public static async UniTask<string> GetSkyBoxKey(string sceneName)
        {
            string skyBoxKey = $"SkyBox_{sceneName}";
            return await CheckKey(skyBoxKey) == skyBoxKey ? skyBoxKey : $"SkyBox_Standard";
        }

        public static async UniTask<string> GetEditorResourcesKey(string groundType)
        {
            return await CheckKey($"Icon{groundType}");
        }

        public static string GetGroundMaterialKey(string groundType, int subType)
        {
            return $"{groundType} {subType}";
        }

        public static string GetEnvironmentObjectKey(int groundId, int number)
        {
            return $"EO_{groundId}{number}";
        }

        public static string GetEnvironmentObjectKey(string id)
        {
            if(!id.Contains("EO"))
                return $"EO_{id}";
            return id;
        }

        private static async UniTask<string> CheckKey(string key)
        {
            if (await AddressablesAssetLoader.CheckKey(key))
                return key;
        
            return null;
        }
    }
}
