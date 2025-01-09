using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
	public class ExitToMenuTrigger : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private CanvasGroup _canvasGroup;

		private float _exitToMenuDelay;

		private float _fillAndAlpha;
		private float _elapsedTime;
		private bool _isRaycasted;

		public bool Activated { get; private set; }

		public void Init(float exitToMenuDelay)
		{
			_exitToMenuDelay = exitToMenuDelay;
		}

		private void Awake()
		{
			ChangeVisibility(_fillAndAlpha);
		}

		private void Update()
		{
			if (!_isRaycasted)
			{
				if (_fillAndAlpha > 0)
					ResetAlphaAndFill();
			}
			else
			{
				UpdateAlphaAndFill();
			}
		}

		public void Increase()
		{
			if (!_isRaycasted)
				_isRaycasted = true;
		}

		public void Reset()
		{
			_isRaycasted = false;
		}

		private void UpdateAlphaAndFill()
		{
			if (Activated)
				return;

			_elapsedTime += Time.deltaTime;
			float progress = Mathf.Clamp01(_elapsedTime / _exitToMenuDelay);
			_fillAndAlpha = progress;

			ChangeVisibility(_fillAndAlpha);

			if (progress >= 1f)
			{
				Activated = true;
			}
		}

		private void ResetAlphaAndFill()
		{
			_elapsedTime = 0;
			_fillAndAlpha = 0;
			Activated = false;
			ChangeVisibility(_fillAndAlpha);
		}

		private void ChangeVisibility(float value)
		{
			_canvasGroup.alpha = value;
			_image.fillAmount = value;
		}
	}
}