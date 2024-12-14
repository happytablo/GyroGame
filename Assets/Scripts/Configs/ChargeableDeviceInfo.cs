using System;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class ChargeableDeviceInfo
	{
		[field: SerializeField] public DeviceType DeviceType { get; private set; }
		[field: SerializeField] public int Amount { get; private set; }
	}
}