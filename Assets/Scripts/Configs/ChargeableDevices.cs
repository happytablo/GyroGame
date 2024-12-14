using System;
using System.Linq;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class ChargeableDevices
	{
		[SerializeField] private ChargeableDeviceInfo[] _chargeableDevices;

		public ChargeableDeviceInfo GetDeviceInfo(DeviceType deviceType)
		{
			return _chargeableDevices.FirstOrDefault(info => info.DeviceType == deviceType);
		}
	}
}