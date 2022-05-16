using System;
using Modules.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Modules.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour
  {
    [Inject]
    public async void Constructor(GameStateMachine gameStateMachine) =>
      await gameStateMachine.Enter<BootstrapState>();
  }
}