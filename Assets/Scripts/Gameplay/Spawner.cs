using System.Collections.Generic;
using Configs;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Gameplay
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private Transform _cloudsContainer;
		[SerializeField] private Transform _sunbeamsContainer;

		private Config _config;
		private CloudMover _lastSpawnedCloud;
		private readonly List<Sunbeam> _sunbeams = new List<Sunbeam>();
		
		public void Init(Config config)
		{
			_config = config;

			SpawnSunbeams();
		}

		public void InitLevel(LevelConfig levelConfig)
		{
			SpawnClouds(levelConfig);

			foreach (var sunbeam in _sunbeams)
			{
				sunbeam.Enable();
			}
		}

		public void DisableSunbeams()
		{
			foreach (var sunbeam in _sunbeams)
			{
				sunbeam.Disable();
			}
		}

		public void CleanupClouds()
		{
			foreach (Transform child in _cloudsContainer)
			{
				Destroy(child.gameObject);
			}
		}

		private void SpawnSunbeams()
		{
			float accumulatedDistance = _config.MovementBordersAxisX.x;
			float sunbeamWidth = _config.SunbeamPrefab.transform.localScale.x;

			while (accumulatedDistance < _config.MovementBordersAxisX.y)
			{
				var sunbeamSpawnPos = _config.SunbeamSpawnPos;
				var spawnPos = new Vector3(accumulatedDistance, sunbeamSpawnPos.y, sunbeamSpawnPos.z);
				var sunbeam = Instantiate(_config.SunbeamPrefab, spawnPos, Quaternion.Euler(_config.SunbeamRotation), _sunbeamsContainer).GetComponent<Sunbeam>();
				_sunbeams.Add(sunbeam);
				accumulatedDistance += sunbeamWidth;
			}
		}

		private void SpawnClouds(LevelConfig levelConfig)
		{
			float accumulatedDistance = _config.MovementBordersAxisX.x + _config.CloudOffsetX;

			while (accumulatedDistance < _config.MovementBordersAxisX.y)
			{
				_lastSpawnedCloud = GetOriginalCloud();
				var cloudWidth = _lastSpawnedCloud.transform.localScale.x;
				var spawnPos = _lastSpawnedCloud.transform.position;
				var spawnInterval = Random.Range(levelConfig.CloudSpawnIntervalRange.x, levelConfig.CloudSpawnIntervalRange.y);

				var accumulatedSpawnPos = new Vector3(accumulatedDistance, spawnPos.y, spawnPos.z);
				var cloudMover = Instantiate(_lastSpawnedCloud, accumulatedSpawnPos, Quaternion.identity, _cloudsContainer).GetComponent<CloudMover>();
				cloudMover.Init(_config.MovementBordersAxisX.y);

				accumulatedDistance += cloudWidth / 2 + spawnInterval;
			}
		}

		private CloudMover GetOriginalCloud()
		{
			CloudMover cloudPrefab;
			do
			{
				cloudPrefab = _config.CloudPrefabs.GetRandomElement();
			}
			while (cloudPrefab == _lastSpawnedCloud);

			return cloudPrefab;
		}
	}
}