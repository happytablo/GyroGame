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

		private int _currentLevelIndex = 0;

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
			StartLevel(_currentLevelIndex);
		}

		public void RestartGame()
		{
			_timer.Finished -= Finish;
			_solarBattery.Fulled -= Finish;

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
			_solarBattery.Subscribe(levelConfig.ChargingStepPerFrame);
			_cameraMover.enabled = true;

			_timer.Finished += Finish;
			_solarBattery.Fulled += Finish;

			Started?.Invoke();
		}

		private void Finish()
		{
			_timer.Finished -= Finish;
			_solarBattery.Fulled -= Finish;

			_timer.Stop();
			_solarBattery.Unsubscribe();
			_spawner.Cleanup();
			_cameraMover.enabled = false;

			Finished?.Invoke();
		}
	}
}