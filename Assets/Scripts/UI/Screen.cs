using Gameplay;
using Structure;
using UnityEngine;

namespace UI
{
	public class Screen : MonoBehaviour
	{
		[SerializeField] private GameObject _ingameView;
		[SerializeField] private EndGameView _endGameView;
		[Space]
		[SerializeField] private TimerView _timerView;
		[SerializeField] private BatteryView _batteryView;

		private IGameplayManager _gameplayManager;
		private bool _isSubscribed;

		public void Init(Timer timer, SolarBattery solarBattery, IGameplayManager gameplayManager)
		{
			_gameplayManager = gameplayManager;

			_timerView.Init(timer);
			_batteryView.Init(solarBattery);
			_endGameView.Init(gameplayManager, solarBattery);

			Subscribe();
		}

		private void OnEnable()
		{
			if (_gameplayManager != null)
				Subscribe();
		}

		private void OnDisable()
		{
			Unsubscribe();
		}

		private void OnStarted()
		{
			_ingameView.gameObject.SetActive(true);
			_endGameView.gameObject.SetActive(false);
		}

		private void OnFinished()
		{
			_ingameView.gameObject.SetActive(false);
			_endGameView.gameObject.SetActive(true);
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				_gameplayManager.Finished += OnFinished;
				_gameplayManager.Started += OnStarted;
				_isSubscribed = true;
			}
		}

		private void Unsubscribe()
		{
			if (_isSubscribed && _gameplayManager != null)
			{
				_gameplayManager.Finished -= OnFinished;
				_gameplayManager.Started -= OnStarted;
				_isSubscribed = false;
			}
		}
	}
}