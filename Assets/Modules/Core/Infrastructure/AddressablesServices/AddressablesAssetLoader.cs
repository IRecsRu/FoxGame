using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Modules.Core.Infrastructure.AddressablesServices
{
    public static class AddressablesAssetLoader
    {
        public static async Task<T> LoadAssetAsync<T>(string key)
        {
            await CheckKeyErrorResult(key);
            IResourceLocation location = await CheckLocation(key);
            AsyncOperationHandle<T> handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(location);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
                return  handle.Result;
            else
                throw new InvalidOperationException(handle.Status.ToString());
        }

        public static async Task<Scene> LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
        {
            AsyncOperationHandle<SceneInstance> sceneHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, loadSceneMode);
            await sceneHandle.Task;
            Scene scene = sceneHandle.Result.Scene;
            UnityEngine.AddressableAssets.Addressables.Release(sceneHandle);
            return scene;
        }

        public static async Task<bool> CheckKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException($"Null key {key}");

            IResourceLocation location = await CheckLocation(key);
            return location != null;
        }

        public static async Task CheckKeyErrorResult(string key)
        {
            if (!await CheckKey(key))
                Debug.LogError($"Key {key} not found. \nTo avoid errors, check the key before using it through the CheckKey method");
        }

        private static async Task<IResourceLocation> CheckLocation(string key)
        {
            AsyncOperationHandle<IList<IResourceLocation>> locationHandle = UnityEngine.AddressableAssets.Addressables.LoadResourceLocationsAsync(key);
            await locationHandle.Task;

            if (locationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                if (locationHandle.Result.Count > 0)
                {
                    IResourceLocation location = locationHandle.Result[0];
                    UnityEngine.AddressableAssets.Addressables.Release(locationHandle);
                    return location;   
                }

                return null;
            }
            else
            {
                throw new InvalidOperationException(locationHandle.Status.ToString());
            }
        }
    }
}

