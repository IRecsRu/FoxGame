using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Modules.Core.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IState> _states = new();
    private IState _activeState;

    public bool TryAddState(Type type, IState state)
    {
      if(_states.ContainsKey(type))
        return false;
      
      _states.Add(type, state);
      return true;
    }
    
    public async Task Enter<TState>() where TState : class, IState =>
      await TryChangeState<TState>();

    private async Task<bool> TryChangeState<TState>() where TState : class, IState
    {
      if(!await TryExitActiveState())
        return false;

      TState state = GetState<TState>();
      _activeState = state;
      await state.Enter();
      
      return true;
    }
    
    private async Task<bool> TryExitActiveState()
    {
      if(_activeState != null)
      {
        if(!await _activeState.TryExit())
        {
          Debug.LogError("Cant changeState!");
          return false;
        }
      }
      return true;
    }

    private TState GetState<TState>() where TState : class, IState => 
      _states[typeof(TState)] as TState;
  }
}