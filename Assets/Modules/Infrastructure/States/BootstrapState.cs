using System.Threading.Tasks;
using Modules.Infrastructure.LevelLoader;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Modules.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public async Task Enter()
    {
      await ProjectInitialization();
      await _stateMachine.Enter<LoginState>();
    }

    public async Task<bool> TryExit() =>
      true;

    private async Task ProjectInitialization()
    {
      await Addressables.InitializeAsync().Task;
    }

  }

}