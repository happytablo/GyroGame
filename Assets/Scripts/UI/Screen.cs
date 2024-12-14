using Configs;
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
		[SerializeField] private LevelProgressView _levelProgressView;
		[SerializeField] private LevelFinishView _levelFinishView;

		private IGameplayManager _gameplayManager;
		private Config _config;

		private bool _isSubscribed;

		public void Init(Timer timer, SolarBattery solarBattery, IGameplayManager gameplayManager, Config config)
		{
			_gameplayManager = gameplayManager;
			_config = config;

			_timerView.Init(timer);
			_batteryView.Init(solarBattery);
			_endGameView.Init(gameplayManager);
			_levelProgressView.Init(gameplayManager);

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
			_endGameView.UpdateDevicesInfo();
		}

		private void OnLevelFinished(bool isWon)
		{
			int currentLevel = _gameplayManager.CurrentLevelIndex + 1;
			_levelFinishView.Show(currentLevel, _config.PauseBetweenLevelsDuration, isWon);
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				_gameplayManager.Finished += OnFinished;
				_gameplayManager.Started += OnStarted;
				_gameplayManager.LevelFinished += OnLevelFinished;
				_isSubscribed = true;
			}
		}

		private void Unsubscribe()
		{
			if (_isSubscribed && _gameplayManager != null)
			{
				_gameplayManager.Finished -= OnFinished;
				_gameplayManager.Started -= OnStarted;
				_gameplayManager.LevelFinished -= OnLevelFinished;
				_isSubscribed = false;
			}
		}
	}
}