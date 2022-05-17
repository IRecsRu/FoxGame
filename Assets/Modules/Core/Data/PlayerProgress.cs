using System;
using Modules.Core.StaticData;

namespace Modules.Core.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public HealthState HealthState;
    public AttackStats AttackStats;
    public MoveStats MoveStats;
    public WorldData WorldData;

    public PlayerProgress(string initialLevel, PlayerStaticData playerStaticData)
    {
      HealthState = playerStaticData.HealthState;
      AttackStats = playerStaticData.AttackStats;
      MoveStats = playerStaticData.MoveStats;

      WorldData = new WorldData(initialLevel);
    }
  }
}