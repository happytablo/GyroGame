using System.Collections;
using Configs;
using Gameplay;
using UnityEngine;
using Screen = UI.Screen;

namespace Structure.GameStates
{
	public class GameLoopState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly Screen _screen;
		private readonly Config _config;
		private readonly GameplayManager _gameplayManager;
		private readonly ICoroutineRunner _coroutineRunner;

		private Coroutine _coroutine;

		public GameLoopState(IStateSwitcher stateSwitcher, Screen screen, Config config, GameplayManager gameplayManager, ICoroutineRunner coroutineRunner)
		{
			_stateSwitcher = stateSwitcher;
			_screen = screen;
			_config = config;
			_gameplayManager = gameplayManager;
			_coroutineRunner = coroutineRunner;
		}

		public void Enter()
		{
			DeviceGyro.EnableGyro();

			_gameplayManager.LevelFinished += OnLevelFinished;
			_gameplayManager.InitLevel();
			_screen.OnStarted();
		}

		public void Exit()
		{
			DeviceGyro.DisableGyro();

			_gameplayManager.LevelFinished -= OnLevelFinished;
		}

		private void OnLevelFinished(bool isWon)
		{
			_screen.OnLevelFinished(isWon);

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
				_gameplayManager.FinishGame();
				_stateSwitcher.ChangeState<FinishState>();
			}
		}
	}
}