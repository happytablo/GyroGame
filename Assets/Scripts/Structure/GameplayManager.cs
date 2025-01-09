using System;
using Configs;
using Gameplay;
using UnityEngine;

namespace Structure
{
	public class GameplayManager : ILevelInfo
	{
		public event Action<bool> LevelFinished;
		public event Action ExitTriggered;

		private readonly Config _config;
		private readonly Spawner _spawner;
		private readonly Timer _timer;
		private readonly SolarBattery _solarBattery;
		private readonly BuildingsController _buildingsController;
		private readonly RaycastToExitChecker _toExitChecker;

		private int _currentLevelIndex;
		private LevelConfig _currentLevelConfig;
		private Coroutine _levelEndedCoroutine;

		public int CurrentLevelIndex => _currentLevelIndex;
		public LevelConfig CurrentLevelConfig => _currentLevelConfig;
		public bool IsLastLevel => _currentLevelIndex >= _config.LevelsConfigsStorage.LevelConfigs.Count - 1;

		public GameplayManager(Config config, Spawner spawner, Timer timer, SolarBattery solarBattery,
							   BuildingsController buildingsController, RaycastToExitChecker toExitChecker)
		{
			_config = config;
			_spawner = spawner;
			_timer = timer;
			_solarBattery = solarBattery;
			_buildingsController = buildingsController;
			_toExitChecker = toExitChecker;
		}

		public void InitLevel()
		{
			Subscribe();
			StartLevel(_currentLevelIndex);
		}

		public void LoadNextLevel()
		{
			_spawner.CleanupClouds();
			_currentLevelIndex++;
			StartLevel(_currentLevelIndex);
		}

		public void Cleanup()
		{
			Unsubscribe();
			_spawner.Cleanup();
			_solarBattery.Cleanup();
		}

		private void StartLevel(int levelIndex)
		{
			_currentLevelConfig = _config.LevelsConfigsStorage.GetLevelByIndex(levelIndex);
			if (_currentLevelConfig == null)
			{
				Debug.Log("No levels.");
				return;
			}

			_solarBattery.BeginCharging(_currentLevelConfig.ChargingStepPerFrame);
			_timer.StartTimer(_currentLevelConfig.Time);
			_spawner.InitLevel(_currentLevelConfig);
		}

		private void OnBatteryFulled()
		{
			TriggerLevelEnd(true);
		}

		private void OnTimerFinished()
		{
			TriggerLevelEnd(false);
		}

		private void TriggerLevelEnd(bool isWon)
		{
			EndLevel();

			if (isWon)
				_buildingsController.UpdateBuildingsColor(_currentLevelConfig.BuildingsColor);

			LevelFinished?.Invoke(isWon);
		}

		private void EndLevel()
		{
			_timer.Pause(true);
			_spawner.DisableSunbeams();
			_solarBattery.StopCharging();
		}

		private void OnExitTriggered()
		{
			EndLevel();
			ExitTriggered?.Invoke();
		}

		private void Subscribe()
		{
			_timer.Finished += OnTimerFinished;
			_solarBattery.Fulled += OnBatteryFulled;
			_toExitChecker.TriggerToMenu += OnExitTriggered;
		}

		private void Unsubscribe()
		{
			_timer.Finished -= OnTimerFinished;
			_solarBattery.Fulled -= OnBatteryFulled;
			_toExitChecker.TriggerToMenu -= OnExitTriggered;
		}
	}
}