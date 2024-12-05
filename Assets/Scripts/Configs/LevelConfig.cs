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
		[Header("Sunbeams")]
		[SerializeField] private float _sunbeamIntervalX = 5f;
		[SerializeField] [Range(0f, 1f)] private float _chargeableProbabilityCoef = 0.8f;
		[Header("Clouds")]
		[SerializeField] private float _cloudSpawnInterval = 3f;
		[SerializeField] private float _cloudSpeed = 0.5f;

		public float SunbeamIntervalX => _sunbeamIntervalX;
		public float ChargeableProbabilityCoef => _chargeableProbabilityCoef;
		public float CloudSpawnInterval => _cloudSpawnInterval;
		public float CloudSpeed => _cloudSpeed;
		public float Time => _time;
		public float ChargingStepPerFrame => _chargingStepPerFrame;
	}
}