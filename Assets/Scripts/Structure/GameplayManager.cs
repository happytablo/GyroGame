using System;
using System.Collections;
using Configs;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Structure
{
	public class GameplayManager : IGameplayManager
	{
		public event Action Started;
		public event Action<bool> LevelFinished;
		public event Action Finished;

		private readonly Config _config;
		private readonly Spawner _spawner;
		private readonly Timer _timer;
		private readonly SolarBattery _solarBattery;
		private readonly CameraMover _cameraMover;
		private readonly ICoroutineRunner _coroutineRunner;

		private int _currentLevelIndex;
		private LevelConfig _currentLevelConfig;
		private Coroutine _levelEndedCoroutine;

		public int CurrentLevelIndex => _currentLevelIndex;
		public LevelConfig CurrentLevelConfig => _currentLevelConfig;
		public bool IsLastLevel => _currentLevelIndex >= _config.LevelsConfigsStorage.LevelConfigs.Count - 1;

		public GameplayManager(Config config, Spawner spawner, Timer timer, SolarBattery solarBattery,
							   CameraMover cameraMover, ICoroutineRunner coroutineRunner)
		{
			_config = config;
			_spawner = spawner;
			_timer = timer;
			_solarBattery = solarBattery;
			_cameraMover = cameraMover;
			_coroutineRunner = coroutineRunner;
		}

		public void InitLevel()
		{
			Subscribe();
			StartLevel(_currentLevelIndex);
		}

		public void RestartGame() => SceneManager.LoadScene(0);

		public void LoadNextLevel()
		{
			CleanupCurrentLevel();
			_currentLevelIndex++;
			StartLevel(_currentLevelIndex);
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
			_cameraMover.enabled = true;

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
			if (_levelEndedCoroutine != null)
				_coroutineRunner.StopCoroutine(_levelEndedCoroutine);

			_levelEndedCoroutine = _coroutineRunner.StartCoroutine(OnLevelEndedCoroutine(isWon));
		}

		private void FinishGame()
		{
			Unsubscribe();
			_spawner.CleanupClouds();
			Finished?.Invoke();
		}

		private void EndLevel()
		{
			_timer.Pause(true);
			_spawner.DisableSunbeams();
			_solarBattery.StopCharging();
			_cameraMover.enabled = false;
		}

		private IEnumerator OnLevelEndedCoroutine(bool isWon)
		{
			EndLevel();
			LevelFinished?.Invoke(isWon);

			yield return new WaitForSeconds(_config.PauseBetweenLevelsDuration);

			if (isWon && _config.LevelsConfigsStorage.HasNextLevel(_currentLevelIndex))
				LoadNextLevel();
			else
				FinishGame();
		}

		private void CleanupCurrentLevel() =>
			_spawner.CleanupClouds();

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