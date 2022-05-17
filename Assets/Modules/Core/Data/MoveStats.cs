using System;
using UnityEngine;

namespace Modules.Core.Data
{
	[Serializable]
	public class MoveStats
	{
		[Range(1, 10)]
		public float MoveSpeed = 5f;
	}
}