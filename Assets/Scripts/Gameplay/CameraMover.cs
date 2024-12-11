using Configs;
using UnityEngine;

namespace Gameplay
{
	public class CameraMover : MonoBehaviour
	{
		[SerializeField] private float _speedCoef = 10;

		private Config _config;

		private const float InitialGyroX = 0;

		public void Init(Config config)
		{
			_config = config;
		}

		private void Update()
		{
			if (!DeviceGyro.HasGyroscope)
				return;

			float currentGyroX = DeviceGyro.Gyroscope.gravity.x;

			float deltaX = currentGyroX - InitialGyroX;

			if (deltaX > 180)
				deltaX += 360;

			if (deltaX < -180)
				deltaX -= 360;

			float moveX = deltaX * _speedCoef * Time.deltaTime;

			var targetPosX = Mathf.Clamp(transform.position.x + moveX, _config.MovementBordersAxisX.x, _config.MovementBordersAxisX.y);
			transform.position = new Vector3(targetPosX, transform.position.y, transform.position.z);
		}
	}
}