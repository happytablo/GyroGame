using Gameplay;
using TMPro;
using UnityEngine;

namespace UI
{
	public class TimerView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		private Timer _timer;
		private bool _isSubscribed;

		public void Init(Timer timer)
		{
			_timer = timer;

			Subscribe();
		}

		private void OnEnable()
		{
			if (_timer != null)
			{
				UpdateView();
				Subscribe();
			}
		}

		private void OnDisable()
		{
			if (_isSubscribed)
			{
				_timer.Ticked -= UpdateView;
				_isSubscribed = false;
			}
		}

		private void UpdateView()
		{
			var totalSeconds = Mathf.FloorToInt(_timer.RemainingTime);
			int seconds = totalSeconds % 60;
			int minutes = (totalSeconds / 60) % 60;
			_text.text = $"{minutes:00}:{seconds:00}";
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				_timer.Ticked += UpdateView;
				_isSubscribed = true;
			}
		}
	}
}