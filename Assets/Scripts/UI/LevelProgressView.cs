using Structure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class LevelProgressView : MonoBehaviour
	{
		[SerializeField] private Image[] _levelStepImages;
		[SerializeField] private bool _isEndView;
		[Space]
		[SerializeField] private Color _lockedColor;
		[SerializeField] private Color _passedColor;

		private IGameLoop _gameLoop;
		private bool _isSubscribed;

		public void Init(IGameLoop gameLoop)
		{
			_gameLoop = gameLoop;

			UpdateView();
			Subscribe();
		}

		private void OnEnable()
		{
			if (_gameLoop != null)
			{
				UpdateView();
				Subscribe();
			}
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				_gameLoop.Started += UpdateView;
				_isSubscribed = true;
			}
		}

		private void OnDisable()
		{
			if (_isSubscribed)
			{
				_gameLoop.Started -= UpdateView;
				_isSubscribed = false;
			}
		}

		private void UpdateView()
		{
			var currentLevelIndex = _gameLoop.CurrentLevelIndex;
			var isLastLevel = _gameLoop.IsLastLevel;

			for (int i = 0; i < _levelStepImages.Length; i++)
			{
				bool isPassed = i < currentLevelIndex;
				var color = isPassed ? _passedColor : _lockedColor;

				if (!isPassed && isLastLevel && _isEndView)
					color = _passedColor;

				_levelStepImages[i].color = color;
			}
		}
	}
}