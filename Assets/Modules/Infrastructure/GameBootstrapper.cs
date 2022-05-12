using Modules.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Modules.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour
  {
    [Inject]
    public void Constructor(GameStateMachine gameStateMachine) =>
      gameStateMachine.Enter<BootstrapState>();
  }
}