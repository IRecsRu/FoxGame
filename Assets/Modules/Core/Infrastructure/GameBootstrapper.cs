using Modules.Core.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Modules.Core.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour
  {
    [Inject]
    public async void Constructor(GameStateMachine gameStateMachine) =>
      await gameStateMachine.Enter<BootstrapState>();
  }
}