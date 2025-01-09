using UnityEngine;

namespace Gameplay
{
	public static class DeviceGyro
	{
		public static bool HasGyroscope => SystemInfo.supportsGyroscope;
		public static bool Enabled { get; private set; }

		public static Gyroscope Gyroscope;

		public static void EnableGyro()
		{
			if (HasGyroscope)
			{
				Gyroscope ??= Input.gyro;
				Gyroscope.enabled = true;
				Enabled = true;
			}
		}

		public static void DisableGyro()
		{
			if (HasGyroscope)
			{
				if (Gyroscope == null)
					return;

				Gyroscope.enabled = false;
				Enabled = false;
			}
		}
	}
}