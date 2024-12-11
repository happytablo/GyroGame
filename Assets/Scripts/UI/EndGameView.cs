using System;
using Structure;
using UnityEngine;

namespace UI
{
	public class EndGameView : MonoBehaviour
	{
		[SerializeField] private GyroButton[] _gyroButtons;
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