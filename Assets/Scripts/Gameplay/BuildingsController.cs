using System.Collections.Generic;
using System.Linq;
using Structure;
using UnityEngine;

namespace Gameplay
{
	public class BuildingsController : MonoBehaviour
	{
		private List<MeshRenderer> _buildingMeshes;
		private IGameLoop _gameLoop;

		public void Init(IGameLoop gameLoop)
		{
			_gameLoop = gameLoop;

			_gameLoop.LevelFinished += OnLevelFinished;
		}

		private void Awake()
		{
			_buildingMeshes = GetComponentsInChildren<MeshRenderer>().ToList();
		}

		private void OnDestroy()
		{
			if (_gameLoop != null)
			{
				_gameLoop.LevelFinished -= OnLevelFinished;
			}
		}

		private void OnLevelFinished(bool isWon)
		{
			if (!isWon)
				return;

			foreach (MeshRenderer buildingMesh in _buildingMeshes)
			{
				buildingMesh.material.color = _gameLoop.CurrentLevelConfig.BuildingsColor;
			}
		}
	}
}