using Configs;
using Gameplay;
using UnityEngine;
using Screen = UI.Screen;
using Timer = Gameplay.Timer;

namespace Structure
{
	public class EntryPoint : MonoBehaviour
	{
		[Header("Configs")]
		[SerializeField] private Config _config;
		[SerializeField] private LevelConfigsStorage _levelConfigsStorage;

		[Space]
		[SerializeField] private CameraMover _cameraMover;
		[SerializeField] private Spawner _spawner;
		[SerializeField] private SolarBattery _solarBattery;
		[SerializeField] private Timer _timer;
		[SerializeField] private BuildingsController _buildingsController;

		[Header("UI")]
		[SerializeField] private Screen _screenPrefab;

		private void Awake()
		{
			DeviceGyro.EnableGyro();

			_cameraMover.Init(_config);
			_spawner.Init(_config);
			var gameplayManager = new GameplayManager(_levelConfigsStorage, _spawner, _timer, _solarBattery, _cameraMover);
			_buildingsController.Init(gameplayManager);
			InitScreen(gameplayManager);

			gameplayManager.InitLevel();
		}

		private void InitScreen(GameplayManager gameplayManager)
		{
			Screen screen = Instantiate(_screenPrefab).GetComponent<Screen>();
			screen.Init(_timer, _solarBattery, gameplayManager);
		}
	}
}