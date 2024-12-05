using System;
using Gameplay;
using UnityEngine;
using Utils;

namespace UI
{
	public class GyroButton : MonoBehaviour
	{
		[SerializeField] private SlicedFilledImage _filledImage;
		[SerializeField] private GyroButtonType _gyroButtonType;
		[Space]
		[SerializeField] private float _fillSpeed = 0.5f;
		[SerializeField] private float _resetSpeed = 1f;
		[SerializeField] private float _tiltThreshold = 0.15f;

		public event Action<GyroButtonType> Filled;

		private void OnEnable()
		{
			_filledImage.fillAmount = 0;
		}

		private void Update()
		{
			if (DeviceGyro.HasGyroscope)
			{
				HandleGyroInput();
			}
		}

		private void HandleGyroInput()
		{
			float tiltX = DeviceGyro.Gyroscope.gravity.x;

			if (_gyroButtonType == GyroButtonType.Restart && tiltX < -_tiltThreshold)
			{
				_filledImage.fillAmount += _fillSpeed * Time.deltaTime;
			}
			else if (_gyroButtonType == GyroButtonType.Next && tiltX > _tiltThreshold)
			{
				_filledImage.fillAmount += _fillSpeed * Time.deltaTime;
			}
			else
			{
				_filledImage.fillAmount -= _resetSpeed * Time.deltaTime;
			}

			if (_filledImage.fillAmount >= 1)
			{
				Filled?.Invoke(_gyroButtonType);
			}

			_filledImage.fillAmount = Mathf.Clamp01(_filledImage.fillAmount);
		}
	}
}