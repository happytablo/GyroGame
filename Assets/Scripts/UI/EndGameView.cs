using System;
using Gameplay;
using Structure;
using UnityEngine;
using Utils;

namespace UI
{
	public class EndGameView : MonoBehaviour
	{
		[SerializeField] private GyroButton[] _gyroButtons;
		[SerializeField] private SlicedFilledImage _batteryFilledImage;

		private IGameplayManager _gameplayManager;
		private SolarBattery _solarBattery;
		private bool _isSubscribed;

		public void Init(IGameplayManager gameplayManager, SolarBattery solarBattery)
		{
			_gameplayManager = gameplayManager;
			_solarBattery = solarBattery;
		}

		private void OnEnable()
		{
			Subscribe();

			if (_solarBattery != null)
				_batteryFilledImage.fillAmount = _solarBattery.ChargeValue / 1;
		}

		private void OnDisable()
		{
			Unsubscribe();
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