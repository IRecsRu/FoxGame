using System;
using UnityEngine;

namespace Modules.Core.Infrastructure.AddressablesServices
{
    public class NotifyOnDestroy : MonoBehaviour
    {
        public event Action<string, NotifyOnDestroy> Destroyed;
        private string _key = null;

        public void Initialization(string key)
        {
            if (_key != null)
                throw new ArgumentException(nameof(_key) + " is dont null");
            _key = key;
        }

        public void OnDestroy()
        {
            Destroyed?.Invoke(_key, this);
        }
    }
}