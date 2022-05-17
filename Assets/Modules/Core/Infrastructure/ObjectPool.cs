using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modules.Core.Infrastructure.AddressablesServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Core.Infrastructure
{
	public class ObjectPool<T> : IDisposable where T : Component 
	{
		private readonly string _key;
		private readonly Transform _container;
		private readonly AddressablesGameObjectLoader _loader = new();
		
		private readonly List<T> _objects = new();
		
		public ObjectPool(string key, Transform container)
		{
			_key = key;
			_container = container;
		}

		public async Task<T> GetObject()
		{
			T returnObject = _objects.FirstOrDefault(p => !p.gameObject.activeSelf);

			if(returnObject == null)
				returnObject = await CreateObject();

			returnObject.gameObject.SetActive(true);
			return returnObject;
		}

		private async Task<T> CreateObject()
		{
			T returnObject = await _loader.Instantiate<T>(_container, _key);
			returnObject.gameObject.SetActive(false);
			_objects.Add(returnObject);
			return returnObject;
		}
		
		public void Dispose()
		{
			foreach(T panel in _objects)
				Object.Destroy(panel.gameObject);

			_objects.Clear();
		}
	}
}