using System;
using UnityEngine;

namespace Modules.Core.Data
{
  [Serializable]
  public class AttackStats
  {
    [Range(3f, 30f)]
    public float Damage = 10f;
    [Range(0.1f, 5f)]
    public float DamageRadius = 1f;
  }

}