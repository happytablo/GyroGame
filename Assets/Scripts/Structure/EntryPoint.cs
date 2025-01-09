using Configs;
using Gameplay;
using Structure.GameStates;
using UI;
using UnityEngine;
using Timer = Gameplay.Timer;

namespace Structure
{
	public class EntryPoint : MonoBehaviour, ICoroutineRunner
	{
		[Header("Configs")]
		[SerializeField] private Config _config;
		[Space]
		[SerializeField] private CameraMover _cameraMover;
		[SerializeField] private RaycastToExitChecker _toExitChecker;
		[SerializeField] private ExitToMenuTrigger[] _exitToMenuTriggers;
		[SerializeField] private Spawner _spawner;
		[SerializeField] private SolarBattery _solarBattery;
		[SerializeField] private Timer _timer;
		[SerializeField] private BuildingsController _buildingsController;

		[Header("UI")]
		[SerializeField] private ScreenManager _screenManagerPrefab;

		private void Awake()
		{
			_cameraMover.Init(_config);
			_spawner.Init(_config);
			InitExitToMenuTriggers();
			
			var gameplayManager = new GameplayManager(_config, _spawner, _timer, _solarBattery, _buildingsController, _toExitChecker);

			var screen = InitScreen(gameplayManager);

			var gameStateMachine = new GameStateMachine(_config, screen, gameplayManager, this);

			gameStateMachine.ChangeState<PreviewState>();
		}

		private ScreenManager InitScreen(GameplayManager gameplayManager)
		{
			ScreenManager screenManager = Instantiate(_screenManagerPrefab).GetComponent<ScreenManager>();
			screenManager.Init(_timer, _solarBattery, gameplayManager, _config);
			return screenManager;
		}

		private void InitExitToMenuTriggers()
		{
			foreach (var toMenuTrigger in _exitToMenuTriggers)
			{
				toMenuTrigger.Init(_config.TimingConfig.ExitToMenuDelay);
			}
		}
	}
}