using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Modules.Infrastructure.LevelLoader
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