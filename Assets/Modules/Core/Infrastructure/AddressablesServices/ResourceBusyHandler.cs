using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.Core.Infrastructure.AddressablesServices
{
    public class ResourceBusyHandler
    {
        private Dictionary<string, List<GameObject>> _trackedGameObject =
        new Dictionary<string, List<GameObject>>();

        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _asyncOperationHandles =
        new Dictionary<string, AsyncOperationHandle<GameObject>>();

        public bool CheckOperationHandle(string key) => _asyncOperationHandles.ContainsKey(key);

        public void AddOperationHandle(string key, AsyncOperationHandle<GameObject> operationHandle)
        {
            if (!_asyncOperationHandles.ContainsKey(key))
                _asyncOperationHandles.Add(key, operationHandle);
            else
                UnityEngine.AddressableAssets.Addressables.Release(operationHandle);
        }

        public void AddGameObject(string key, GameObject gameObject)
        {
            if (!_trackedGameObject.ContainsKey(key))
                _trackedGameObject.Add(key, new List<GameObject>());

            _trackedGameObject[key].Add(gameObject);
            NotifyOnDestroy notifyOnDestroy = gameObject.AddComponent<NotifyOnDestroy>();
            notifyOnDestroy.Initialization(key);
            notifyOnDestroy.Destroyed += Remove;
        }

        private void Remove(string key, NotifyOnDestroy obj)
        {
            UnityEngine.AddressableAssets.Addressables.ReleaseInstance(obj.gameObject);

            _trackedGameObject[key].Remove(obj.gameObject);

            if (_trackedGameObject[key].Count == 0)
            {
#if Logs_Addressables
                Debug.Log($"Removed all {key}");
#endif
                if (_asyncOperationHandles.ContainsKey(key))
                    UnityEngine.AddressableAssets.Addressables.Release(_asyncOperationHandles[key]);

                _asyncOperationHandles.Remove(key);
#if Logs_Addressables
                if (!_asyncOperationHandles.ContainsKey(key))
                    Debug.Log($"Removed all {key} handles");
#endif
            }
        }
    }
}

