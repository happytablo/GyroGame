using System.Collections;
using Configs;
using Gameplay;
using UI;
using UnityEngine;

namespace Structure.GameStates
{
	public class GameLoopState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly ScreenManager _screenManager;
		private readonly Config _config;
		private readonly GameplayManager _gameplayManager;
		private readonly ICoroutineRunner _coroutineRunner;

		private Coroutine _coroutine;

		public GameLoopState(IStateSwitcher stateSwitcher, ScreenManager screenManager, Config config, GameplayManager gameplayManager, ICoroutineRunner coroutineRunner)
		{
			_stateSwitcher = stateSwitcher;
			_screenManager = screenManager;
			_config = config;
			_gameplayManager = gameplayManager;
			_coroutineRunner = coroutineRunner;
		}

		public void Enter()
		{
			DeviceGyro.EnableGyro();

			_gameplayManager.LevelFinished += OnLevelFinished;
			_gameplayManager.ExitTriggered += OnExitTriggered;
			_gameplayManager.InitLevel();
			_screenManager.OnStarted();
		}

		public void Exit()
		{
			DeviceGyro.DisableGyro();
			
			_gameplayManager.Cleanup();
			_gameplayManager.LevelFinished -= OnLevelFinished;
			_gameplayManager.ExitTriggered -= OnExitTriggered;
		}

		private void OnExitTriggered()
		{
			_stateSwitcher.ChangeState<MenuState>();
		}

		private void OnLevelFinished(bool isWon)
		{
			_screenManager.OnLevelFinished(isWon);

			if (_coroutine != null)
				_coroutineRunner.StopCoroutine(_coroutine);

			_coroutineRunner.StartCoroutine(OnLevelFinishedCoroutine(isWon));
		}

		private IEnumerator OnLevelFinishedCoroutine(bool isWon)
		{
			yield return new WaitForSeconds(_config.TimingConfig.PauseBetweenLevels);

			if (isWon && _config.LevelsConfigsStorage.HasNextLevel(_gameplayManager.CurrentLevelIndex))
			{
				_gameplayManager.LoadNextLevel();
			}
			else
			{
				_stateSwitcher.ChangeState<FinishState>();
			}
		}
	}
}