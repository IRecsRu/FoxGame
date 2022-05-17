using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public static class TypeFinderOnScene
	{
		public static bool TryGetType<T>(out T type, Scene scene)
		{
			GameObject[] roots = scene.GetRootGameObjects();
			type = default( T );
      
			foreach(GameObject t in roots)
			{
				if(t.TryGetComponent(out type))
					return true;
			}

			return false;
		}
	}
}