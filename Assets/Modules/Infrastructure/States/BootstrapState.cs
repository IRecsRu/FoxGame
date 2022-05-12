using Cysharp.Threading.Tasks;
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

    public async UniTask Enter()
    {
      if(SceneManager.GetActiveScene().buildIndex != 0)
        await SceneManager.LoadSceneAsync(0);
      
      await ProjectInitialization();
      //_stateMachine.Enter<LoginState>();
    }

    public bool TryExit() =>
      true;

    private async UniTask ProjectInitialization()
    {
      await Addressables.InitializeAsync();
    }

  }

}