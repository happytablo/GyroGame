using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		
		public void Init(Config config)
		{
			_config = config;
		}

		public void InitLevel(LevelConfig levelConfig)
		{
			SpawnClouds(levelConfig);
			SpawnSunbeams(levelConfig);
		}

		public void Cleanup()
		{
			foreach (Transform child in _container)
			{
				Destroy(child.gameObject);
			}
		}

		private void SpawnSunbeams(LevelConfig levelConfig)
		{
			float accumulatedDistance = _config.MovementBordersAxisX.x; //+ levelConfig.SunbeamIntervalX;
			float sunbeamWidth = _config.SunbeamPrefab.transform.localScale.x;

			while (accumulatedDistance < _config.MovementBordersAxisX.y)
			{
				var random = Random.Range(0f, 1f);
				var sunbeamPosZ = levelConfig.ChargeableProbabilityCoef > random
					? _config.ChargeableSunbeamSpawnPosZ
					: _config.NotChargeableSunbeamSpawnPosZ.GetRandomElement();

				var spawnPos = new Vector3(accumulatedDistance, _config.SunbeamSpawnPosY, sunbeamPosZ);
				Instantiate(_config.SunbeamPrefab, spawnPos, Quaternion.Euler(_config.SunbeamRotation), _container);

				accumulatedDistance += sunbeamWidth;
			}
		}

		private void SpawnClouds(LevelConfig levelConfig)
		{
			float accumulatedDistance = _config.MovementBordersAxisX.x + _config.CloudOffsetX;
			float cloudWidth = _config.CloudPrefab.transform.localScale.x;

			while (accumulatedDistance < _config.MovementBordersAxisX.y - _config.CloudOffsetX)
			{
				var spawnPos = new Vector3(accumulatedDistance, _config.CloudSpawnPos.y, _config.CloudSpawnPos.z);
				var cloudMover = Instantiate(_config.CloudPrefab, spawnPos, Quaternion.identity, _container).GetComponent<CloudMover>();
				cloudMover.Init(levelConfig.CloudSpeed, _config.MovementBordersAxisX.y);

				accumulatedDistance += cloudWidth + levelConfig.CloudSpawnInterval;
			}
		}
	}
}