using System;
using Modules.Core.Data;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Logic;
using UnityEngine;

namespace Modules.Core.PlayerLogic
{
  [RequireComponent(typeof(PlayerAnimator))]
  public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
  {
    [SerializeField] private PlayerAnimator _animator;
    
    private HealthState _healthState;

    public event Action HealthChanged;

    public float Current
    {
      get => _healthState.CurrentHP;
      set
      {
        if (value != _healthState.CurrentHP)
        {
          _healthState.CurrentHP = value;
          
          HealthChanged?.Invoke();
        }
      }
    }

    public float Max
    {
      get => _healthState.MaxHP;
      set => _healthState.MaxHP = value;
    }

    private void Awake()
    {
      _animator = GetComponent<PlayerAnimator>();
    }

    public void LoadProgress(PlayerProgress progress)
    {
      _healthState = progress.HealthState;
      HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.HealthState.CurrentHP = Current;
      progress.HealthState.MaxHP = Max;
    }

    public void TakeDamage(float damage)
    {
      if(Current <= 0)
        return;
      
      Current -= damage;
      _animator.PlayHit();
    }
  }
}