using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.Core.Infrastructure.AddressablesServices
{
    public class GameObjectBusyHandler
    {
        private Dictionary<string, List<GameObject>> _trackedObject =
        new Dictionary<string, List<GameObject>>();

        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _asyncOperationHandles =
        new Dictionary<string, AsyncOperationHandle<GameObject>>();

        public bool CheckOperationHandle(string key) => _asyncOperationHandles.ContainsKey(key);

        public void AddOperationHandle(string key, AsyncOperationHandle<GameObject> operationHandle)
        {
            if (!_asyncOperationHandles.ContainsKey(key))
                _asyncOperationHandles.Add(key, operationHandle);
            else
                throw new System.InvalidOperationException($"Key {key} - is not null");
        }

        public void AddGameObject(string key, GameObject gameObject)
        {
            if (!_trackedObject.ContainsKey(key))
                _trackedObject.Add(key, new List<GameObject>());

            _trackedObject[key].Add(gameObject);
            NotifyOnDestroy notifyOnDestroy = gameObject.AddComponent<NotifyOnDestroy>();
            notifyOnDestroy.Initialization(key);
            notifyOnDestroy.Destroyed += Remove;
        }

        private void Remove(string key, NotifyOnDestroy obj)
        {
            UnityEngine.AddressableAssets.Addressables.ReleaseInstance(obj.gameObject);

            _trackedObject[key].Remove(obj.gameObject);

            if (_trackedObject[key].Count == 0)
            {
                Debug.Log($"Removed all {key}");

                if (_asyncOperationHandles.ContainsKey(key))
                    UnityEngine.AddressableAssets.Addressables.Release(_asyncOperationHandles[key]);

                _asyncOperationHandles.Remove(key);

                if (!_asyncOperationHandles.ContainsKey(key))
                    Debug.Log($"Removed all {key} handles");
            }
        }
    }
}

