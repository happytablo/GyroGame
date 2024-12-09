using System;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class LevelConfig
	{
		[Header("Timer")]
		[SerializeField] private float _time = 60f;
		[Header("Battery")]
		[SerializeField] private float _chargingStepPerFrame;
		[Header("Clouds")]
		[SerializeField] private Vector2 _cloudSpawnIntervalRange;

		public Vector2 CloudSpawnIntervalRange => _cloudSpawnIntervalRange;
		public float Time => _time;
		public float ChargingStepPerFrame => _chargingStepPerFrame;
	}
}