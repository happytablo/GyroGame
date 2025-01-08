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

		private ILevelInfo _levelInfo;
		private bool _isSubscribed;

		public void Init(ILevelInfo levelInfo)
		{
			_levelInfo = levelInfo;

			UpdateView();
		}

		public void UpdateView()
		{
			var currentLevelIndex = _levelInfo.CurrentLevelIndex;
			var isLastLevel = _levelInfo.IsLastLevel;

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