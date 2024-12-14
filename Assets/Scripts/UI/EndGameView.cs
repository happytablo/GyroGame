using System;
using Structure;
using UnityEngine;

namespace UI
{
	public class EndGameView : MonoBehaviour
	{
		[SerializeField] private GyroButton[] _gyroButtons;
		[SerializeField] private ChargeableDeviceView[] _chargeableDeviceViews;
		[SerializeField] private LevelProgressView _levelProgressView;

		private IGameplayManager _gameplayManager;
		private bool _isSubscribed;

		public void Init(IGameplayManager gameplayManager)
		{
			_gameplayManager = gameplayManager;
			_levelProgressView.Init(gameplayManager);
		}

		private void OnEnable()
		{
			Subscribe();
		}

		private void OnDisable()
		{
			Unsubscribe();
		}

		public void UpdateDevicesInfo()
		{
			foreach (var chargeableDeviceView in _chargeableDeviceViews)
			{
				var deviceInfo = _gameplayManager.CurrentLevelConfig.Devices.GetDeviceInfo(chargeableDeviceView.DeviceType);
				chargeableDeviceView.UpdateView(deviceInfo.Amount);
			}
		}

		private void OnButtonFilled(GyroButtonType buttonType)
		{
			switch (buttonType)
			{
				case GyroButtonType.Restart:
					_gameplayManager.RestartGame();
					break;
				case GyroButtonType.Next:
					_gameplayManager.LoadNextLevel();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
			}
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				foreach (var gyroButton in _gyroButtons)
				{
					gyroButton.Filled += OnButtonFilled;
				}

				_isSubscribed = true;
			}
		}

		private void Unsubscribe()
		{
			if (_isSubscribed)
			{
				foreach (var gyroButton in _gyroButtons)
				{
					gyroButton.Filled -= OnButtonFilled;
				}

				_isSubscribed = false;
			}
		}
	}
}