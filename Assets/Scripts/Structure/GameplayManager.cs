using System;
using Configs;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Structure
{
	public class GameplayManager : IGameplayManager
	{
		public event Action Finished;
		public event Action Started;

		private readonly LevelConfigsStorage _levelConfigsStorage;
		private readonly Spawner _spawner;
		private readonly Timer _timer;
		private readonly SolarBattery _solarBattery;
		private readonly CameraMover _cameraMover;

		private int _currentLevelIndex;

		public int CurrentLevelIndex => _currentLevelIndex;

		public GameplayManager(LevelConfigsStorage levelConfigsStorage, Spawner spawner, Timer timer, SolarBattery solarBattery, CameraMover cameraMover)
		{
			_levelConfigsStorage = levelConfigsStorage;
			_spawner = spawner;
			_timer = timer;
			_solarBattery = solarBattery;
			_cameraMover = cameraMover;
		}

		public void InitLevel()
		{
			_timer.Finished += FinishGame;
			_solarBattery.Fulled += OnBatteryFulled;

			StartLevel(_currentLevelIndex);
		}

		public void RestartGame()
		{
			SceneManager.LoadScene(0);
		}

		public void LoadNextLevel()
		{
			_currentLevelIndex++;
			StartLevel(_currentLevelIndex);
		}

		private void StartLevel(int levelIndex)
		{
			LevelConfig levelConfig = _levelConfigsStorage.GetLevelByIndex(levelIndex);
			if (levelConfig == null)
			{
				Debug.Log($"No levels");
				return;
			}

			_spawner.InitLevel(levelConfig);
			_timer.StartTimer(levelConfig.Time);
			_solarBattery.BeginCharging(levelConfig.ChargingStepPerFrame);
			_cameraMover.enabled = true;
			Debug.Log($"lvl: {_currentLevelIndex}");
			Started?.Invoke();
		}

		private void OnBatteryFulled()
		{
			FinishLevel();

			if (_levelConfigsStorage.HasNextLevel(_currentLevelIndex))
			{
				LoadNextLevel();
			}
			else
			{
				FinishGame();
			}
		}

		private void FinishGame()
		{
			_timer.Finished -= FinishGame;
			_solarBattery.Fulled -= OnBatteryFulled;

			FinishLevel();

			Finished?.Invoke();
		}

		private void FinishLevel()
		{
			_timer.Pause(true);
			_spawner.Cleanup();
			_cameraMover.enabled = false;
		}
	}
}