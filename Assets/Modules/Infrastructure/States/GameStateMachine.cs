using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
    private IExitableState _activeState;

    public bool TryAddState(Type type, IExitableState state)
    {
      if(_states.ContainsKey(type))
        return false;
      
      _states.Add(type, state);
      return true;
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      if(ChangeState(out TState state))
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      if(ChangeState<TState>(out TState state))
        state.Enter(payload);
    }

    private bool ChangeState<TState>(out TState state) where TState : class, IExitableState
    {
      state = null;

      if(_activeState != null && !_activeState.TryExit())
      {
        Debug.LogError("Cant changeState!");
        return false;
      }

      state = GetState<TState>();
      _activeState = state;
      
      return true;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}