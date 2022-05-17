using System;
using UnityEngine;

namespace Modules.Core.Data
{
  [Serializable]
  public class HealthState
  {
    [NonSerialized] 
    public float CurrentHP;
    [Range(1, 100)]
    public float MaxHP = 100;

    public void ResetHP() =>
      CurrentHP = MaxHP;
  }
}