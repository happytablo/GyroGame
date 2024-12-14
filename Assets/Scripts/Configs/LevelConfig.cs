using System;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class LevelConfig
	{
		[Header("Timer")]
		[SerializeField] private float _time = 30f;
		[Header("Battery")]
		[SerializeField] private float _chargingStepPerFrame;
		[Header("Clouds")]
		[SerializeField] private Vector2 _cloudSpawnIntervalRange;
		[Header("Buildings")]
		[SerializeField] private Color _buildingsColor;
		[Header("Electro devices")]
		[SerializeField] private ChargeableDevices _chargeableDevices;

		public Vector2 CloudSpawnIntervalRange => _cloudSpawnIntervalRange;
		public float Time => _time;
		public float ChargingStepPerFrame => _chargingStepPerFrame;
		public Color BuildingsColor => _buildingsColor;
		public ChargeableDevices Devices => _chargeableDevices;
	}
}