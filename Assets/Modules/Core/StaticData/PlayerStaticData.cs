using Modules.Core.Data;
using UnityEngine;

namespace Modules.Core.StaticData
{
	[CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Player")]
	public class PlayerStaticData : ScriptableObject
	{
		public HealthState HealthState;
		public AttackStats AttackStats;
		public MoveStats MoveStats;
	}
}