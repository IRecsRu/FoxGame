using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.Core.Infrastructure.AddressablesServices
{
    public class AddressablesGameObjectLoader 
    {
        private readonly ResourceBusyHandler _resourceBusyHandler = new ResourceBusyHandler();

        public async Task<T> Instantiate<T>(Transform transform, string key)
        {
            GameObject gameObject = await InstantiateGameObject(transform, key);
            return gameObject.GetComponent<T>();
        }
        
        public async Task<GameObject> InstantiateGameObject(Transform transform, string key)
        {
            await AddressablesAssetLoader.CheckKeyErrorResult(key);
            await PreloadGameObject(key);
            return await Instantiate(key, transform);
        }

        public async Task PreloadGameObject(string key)
        {
            if (!_resourceBusyHandler.CheckOperationHandle(key))
            {
                AsyncOperationHandle<GameObject> handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>(key);
                handle = await LoadAsyncOperation(handle);
                _resourceBusyHandler.AddOperationHandle(key, handle);
            }
        }

        private async Task<GameObject> Instantiate(string key, Transform transform)
        {
            if (await AddressablesAssetLoader.CheckKey(key))
            {
                AsyncOperationHandle<GameObject> prefabHandle = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(key, transform);
                prefabHandle = await LoadAsyncOperation(prefabHandle);

                _resourceBusyHandler.AddGameObject(key, prefabHandle.Result);
                return prefabHandle.Result;
            }
            else
            {
                throw new ArgumentNullException($"Key {key} not found");
            }
        }

        private async Task<AsyncOperationHandle<GameObject>> LoadAsyncOperation(AsyncOperationHandle<GameObject> prefabHandle)
        {
            await prefabHandle.Task;

            if (prefabHandle.Status != AsyncOperationStatus.Succeeded)
                throw new InvalidOperationException(prefabHandle.Status.ToString());

            if (prefabHandle.Result == null)
                    throw new ArgumentNullException("Result is null");

            return prefabHandle;
        }
    }
}

