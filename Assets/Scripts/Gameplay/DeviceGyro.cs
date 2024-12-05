using UnityEngine;

namespace Gameplay
{
	public static class DeviceGyro
	{
		public static bool HasGyroscope => SystemInfo.supportsGyroscope;

		public static Gyroscope Gyroscope;

		public static void EnableGyro()
		{
			if (HasGyroscope)
			{
				Gyroscope = Input.gyro;
				Gyroscope.enabled = true;
			}
		}
	}
}