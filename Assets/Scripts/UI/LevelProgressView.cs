using Gameplay;
using Structure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class LevelProgressView : MonoBehaviour
	{
		[SerializeField] private Image[] _levelStepImages;
		[Space]
		[SerializeField] private Color _lockedColor;
		[SerializeField] private Color _passedColor;

		private IGameplayManager _gameplayManager;
		private bool _isSubscribed;

		public void Init(IGameplayManager gameplayManager)
		{
			_gameplayManager = gameplayManager;

			UpdateView();
			Subscribe();
		}

		private void OnEnable()
		{
			if (_gameplayManager != null)
			{
				UpdateView();
				Subscribe();
			}
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				_gameplayManager.Started += UpdateView;
				_isSubscribed = true;
			}
		}

		private void OnDisable()
		{
			if (_isSubscribed)
			{
				_gameplayManager.Started -= UpdateView;
				_isSubscribed = false;
			}
		}

		private void UpdateView()
		{
			var levelIndex = _gameplayManager.CurrentLevelIndex;

			for (int i = 0; i < _levelStepImages.Length; i++)
			{
				var color = i >= levelIndex ? _lockedColor : _passedColor;
				_levelStepImages[i].color = color;
			}
		}
	}
}