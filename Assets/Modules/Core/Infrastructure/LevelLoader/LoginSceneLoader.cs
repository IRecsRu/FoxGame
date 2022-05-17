using System.Threading.Tasks;
using Modules.LoginScene;
using UnityEngine;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public class LoginSceneLoader : MonoBehaviour, ILevelLoader
	{
		[SerializeField] private LoginPanel _loginPanel;
		
		public async Task Initialization()
		{
			_loginPanel.Initialization();
		}
	}

}