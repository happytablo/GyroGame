using Configs;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Gameplay
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private Transform _container;

		private Config _config;
		private CloudMover _lastSpawnedCloud;

		public void Init(Config config)
		{
			_config = config;
		}

		public void InitLevel(LevelConfig levelConfig)
		{
			SpawnClouds(levelConfig);
			SpawnSunbeams();
		}

		public void Cleanup()
		{
			foreach (Transform child in _container)
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
				var sunbeamPosZ = _config.ChargeableSunbeamSpawnPosZ;
				var spawnPos = new Vector3(accumulatedDistance, _config.SunbeamSpawnPosY, sunbeamPosZ);
				Instantiate(_config.SunbeamPrefab, spawnPos, Quaternion.Euler(_config.SunbeamRotation), _container);

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
				var cloudMover = Instantiate(_lastSpawnedCloud, accumulatedSpawnPos, Quaternion.identity, _container).GetComponent<CloudMover>();
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