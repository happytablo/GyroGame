using System;
using Configs;
using Gameplay;
using UnityEngine;

namespace Structure
{
	public class GameplayManager : ILevelInfo
	{
		public event Action Started;
		public event Action<bool> LevelFinished;

		private readonly Config _config;
		private readonly Spawner _spawner;
		private readonly Timer _timer;
		private readonly SolarBattery _solarBattery;
		private readonly BuildingsController _buildingsController;

		private int _currentLevelIndex;
		private LevelConfig _currentLevelConfig;
		private Coroutine _levelEndedCoroutine;

		public int CurrentLevelIndex => _currentLevelIndex;
		public LevelConfig CurrentLevelConfig => _currentLevelConfig;
		public bool IsLastLevel => _currentLevelIndex >= _config.LevelsConfigsStorage.LevelConfigs.Count - 1;

		public GameplayManager(Config config, Spawner spawner, Timer timer, SolarBattery solarBattery,
							   BuildingsController buildingsController)
		{
			_config = config;
			_spawner = spawner;
			_timer = timer;
			_solarBattery = solarBattery;
			_buildingsController = buildingsController;
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

		public void FinishGame()
		{
			Unsubscribe();
			_spawner.CleanupClouds();
		}

		private void StartLevel(int levelIndex)
		{
			_currentLevelConfig = _config.LevelsConfigsStorage.GetLevelByIndex(levelIndex);
			if (_currentLevelConfig == null)
			{
				Debug.Log("No levels.");
				return;
			}

			_spawner.InitLevel(_currentLevelConfig);
			_timer.StartTimer(_currentLevelConfig.Time);
			_solarBattery.BeginCharging(_currentLevelConfig.ChargingStepPerFrame);

			Started?.Invoke();
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

		private void Subscribe()
		{
			_timer.Finished += OnTimerFinished;
			_solarBattery.Fulled += OnBatteryFulled;
		}

		private void Unsubscribe()
		{
			_timer.Finished -= OnTimerFinished;
			_solarBattery.Fulled -= OnBatteryFulled;
		}
	}
}