using System;
using System.Collections;
using Configs;
using Gameplay;
using Structure;
using UnityEngine;
using Timer = Gameplay.Timer;

namespace UI
{
	public class ScreenManager : MonoBehaviour
	{
		[SerializeField] private GameObject _ingameView;
		[SerializeField] private EndGameView _endGameView;
		[Space]
		[SerializeField] private TimerView _timerView;
		[SerializeField] private BatteryView _batteryView;
		[SerializeField] private LevelProgressView _levelProgressView;
		[SerializeField] private LevelFinishView _levelFinishView;
		[SerializeField] private PreviewScreen _previewScreen;

		private ILevelInfo _levelInfo;
		private Config _config;

		private bool _isSubscribed;

		public void Init(Timer timer, SolarBattery solarBattery, ILevelInfo levelInfo, Config config)
		{
			_levelInfo = levelInfo;
			_config = config;

			_timerView.Init(timer);
			_batteryView.Init(solarBattery);
			_endGameView.Init(levelInfo);
			_levelProgressView.Init(levelInfo);
		}

		public void ShowPreviewScreen(Action onPreviewEnded)
		{
			StartCoroutine(ShowPreviewScreenCoroutine(onPreviewEnded));
		}

		public void ShowFinishView(Action onFinishViewEnded)
		{
			_ingameView.gameObject.SetActive(false);
			_endGameView.gameObject.SetActive(true);
			_endGameView.UpdateView();

			StartCoroutine(FinishDelayCoroutine(onFinishViewEnded));
		}

		public void OnLevelFinished(bool isWon)
		{
			int currentLevel = _levelInfo.CurrentLevelIndex + 1;
			_levelFinishView.Show(currentLevel, _config.TimingConfig.PauseBetweenLevels, isWon);
			_levelProgressView.UpdateView(isWon);
		}

		public void OnStarted()
		{
			_ingameView.gameObject.SetActive(true);
			_endGameView.gameObject.SetActive(false);
		}

		private IEnumerator ShowPreviewScreenCoroutine(Action onPreviewEnded)
		{
			_previewScreen.Enable();
			yield return new WaitForSeconds(_config.TimingConfig.PreviewDelay);
			yield return _previewScreen.StartCountdownCoroutine();

			_previewScreen.Disable();

			onPreviewEnded?.Invoke();
		}

		private IEnumerator FinishDelayCoroutine(Action onFinishViewEnded)
		{
			yield return new WaitForSeconds(_config.TimingConfig.FinishViewDelay);

			onFinishViewEnded?.Invoke();
		}
	}
}